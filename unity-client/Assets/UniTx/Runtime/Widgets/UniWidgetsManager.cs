using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Database;
using UniTx.Runtime.Extensions;
using UniTx.Runtime.IoC;
using UniTx.Runtime.ResourceManagement;

namespace UniTx.Runtime.Widgets
{
    internal sealed class UniWidgetsManager : IWidgetsManager
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly Stack<IWidget> _stack = new();
        private AssetData _assetData;
        private Transform _parent;
        private IResolver _resolver;

        public event Action<Type> OnPush;
        public event Action<Type> OnPop;

        public void Inject(IResolver resolver) => _resolver = resolver;

        public async UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var config = UniTx.Config;
            _assetData = await UniResources.LoadAssetAsync<AssetData>(config.WidgetsAssetDataKey, null, cToken);
            _parent = GameObject.FindGameObjectWithTag(config.WidgetsParentTag).transform;
        }

        public UniTask PushAsync<TWidgetType>(CancellationToken cToken = default)
            where TWidgetType : IWidget
            => PushAsync<TWidgetType>(new VoidWidgetData(), cToken);

        public async UniTask PushAsync<TWidgetType>(IWidgetData widgetData, CancellationToken cToken = default)
            where TWidgetType : IWidget
        {
            await _semaphore.WaitAsync(cToken);
            try
            {
                var widgetType = typeof(TWidgetType);
                var asset = _assetData.GetAsset(widgetType.Name);
                var widget = await UniResources.CreateInstanceAsync<IWidget>(asset.RuntimeKey, _parent, null, cToken);
                _stack.Push(widget);

                if (widget is IWidgetDataReceiver dataReceiver)
                {
                    dataReceiver.SetData(widgetData);
                }

                if (widget is IInjectable injectable)
                {
                    injectable.Inject(_resolver);
                }

                widget.Initialise();
                UniStatics.LogInfo($"{widgetType.Name} pushed.", this);
                OnPush.Broadcast(widgetType);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async UniTask PopWidgetsStackAsync(CancellationToken cToken = default)
        {
            await _semaphore.WaitAsync(cToken);
            try
            {
                if (_stack.TryPop(out var widget))
                {
                    var widgetType = widget.GetType();
                    widget.Reset();
                    UniResources.DisposeInstance(widget.GameObject);
                    UniStatics.LogInfo($"{widgetType.Name} popped.", this);
                    OnPop.Broadcast(widgetType);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IWidget Peek() => _stack.TryPeek(out var widget) ? widget : null;
    }
}
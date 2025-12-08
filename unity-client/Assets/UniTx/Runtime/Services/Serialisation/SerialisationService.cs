using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Services
{
    public class SerialisationService : IService, ISerialisationService
    {
        private const float SaveInterval = 5f;
        private readonly Serialiser _serialiser = new();

        // private Tween _saveTween;

        public void Inject(IResolver resolver) => _serialiser.Inject(resolver);

        public UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            // _saveTween = DOVirtual.DelayedCall(SaveInterval, _serializer.SerialiseDirty, false).SetLoops(-1);
            return UniTask.CompletedTask;
        }

        public void Reset()
        {
            // _saveTween?.Kill();
            _serialiser.Reset();
        }

        public void Save(ISavedData data)
        {
            if (data?.Id == null)
            {
                UniStatics.LogInfo("Failed to save data because data or data Id is null.", this, Color.red);
                return;
            }

            _serialiser.MarkDirty(data);
        }

        public T Load<T>(string id) where T : ISavedData, new()
        {
            if (id == null)
            {
                UniStatics.LogInfo("Failed to load data because data Id is null.", this, Color.red);
                throw new ArgumentNullException(nameof(id));
            }

            return _serialiser.Deserialise<T>(id);
        }
    }
}
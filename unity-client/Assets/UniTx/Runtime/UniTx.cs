using Cysharp.Threading.Tasks;
using System.Threading;
using Unity;
using UniTx.Runtime.IoC;
using UnityEngine;
using UniTx.Runtime.Events;
using UniTx.Runtime.ResourceManagement;
using UniTx.Runtime.Widgets;

namespace UniTx.Runtime
{
    public static class UniTx
    {
        private static bool _initialised = false;

        internal static UniTxConfig Config { get; private set; }

        public static async UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            if (_initialised) return;

            LoadConfig();
            SetupIoC();
            UniEventBus.SetEventBus(new PriorityEventBus());
            await UniResources.InitialiseAsync(new AddressablesLoadingStrategy(), cToken);
            await UniWidgets.InitialiseAsync(new UniWidgetsManager(), cToken);

            _initialised = true;
        }

        private static void LoadConfig() => Config = Resources.Load<UniTxConfig>("UniTxConfig");

        private static void SetupIoC()
        {
            var container = new UnityContainer();
            var resolver = new UniResolver(container);
            var binder = new UniBinder(container);
            binder.BindAsSingleton(resolver);
            UniStatics.Resolver = container.Resolve<IResolver>(); // ensure resolve from container.
        }
    }
}
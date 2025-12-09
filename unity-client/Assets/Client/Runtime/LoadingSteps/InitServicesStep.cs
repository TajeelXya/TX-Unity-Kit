using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UniTx.Runtime;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.IoC;
using UniTx.Runtime.Services;

namespace Client.Runtime
{
    public sealed class InitDependenciesStep : LoadingStepBase, IInjectable
    {
        private IResolver _resolver;

        public void Inject(IResolver resolver) => _resolver = resolver;

        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var services = _resolver.ResolveAll<IService>().ToArray();

            var len = services.Length;
            for (var i = 0; i < len; i++)
            {
                var service = services[i];

                UniStatics.LogInfo($"Initialising ({i + 1}/{len}): concrete<{service.GetType().Name}>", this);

                if (service is IInjectable injectable)
                {
                    injectable.Inject(_resolver);
                }

                await service.InitialiseAsync(cToken);
            }

            UniStatics.LogInfo("Services initialised.", this);
        }
    }
}
using Cysharp.Threading.Tasks;
using System.Threading;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.Content;

namespace Client.Runtime
{
    public sealed class ContentRegistryStep : LoadingStepBase
    {
        public override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            ContentRegistry.Register<DemoContentData>("DemoContentData");
            return UniTask.CompletedTask;
        }
    }
}
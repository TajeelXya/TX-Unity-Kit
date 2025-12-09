using Cysharp.Threading.Tasks;
using System.Threading;
using UniTx.Runtime;
using UniTx.Runtime.Bootstrap;

namespace Client.Runtime
{
    public sealed class UniTxStep : LoadingStepBase
    {
        public override UniTask InitialiseAsync(CancellationToken cToken = default) => UNITX.InitialiseAsync(cToken);
    }
}
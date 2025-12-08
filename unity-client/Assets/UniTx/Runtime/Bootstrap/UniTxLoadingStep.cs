using Cysharp.Threading.Tasks;
using System.Threading;

namespace UniTx.Runtime.Bootstrap
{
    internal sealed class UniTxLoadingStep : UniLoadingStep
    {
        public override UniTask InitialiseAsync(CancellationToken cToken = default) => UniTx.InitialiseAsync(cToken);
    }
}
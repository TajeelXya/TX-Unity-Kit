using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace UniTx.Runtime.Bootstrap
{
    public abstract class UniLoadingStep : MonoBehaviour, IInitialisableAsync
    {
        public abstract UniTask InitialiseAsync(CancellationToken cToken = default);
    }
}

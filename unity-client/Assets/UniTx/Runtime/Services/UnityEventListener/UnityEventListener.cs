using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniTx.Runtime.Extensions;
using UnityEngine;

namespace UniTx.Runtime.Services
{
    public sealed class UnityEventListener : IService, IUnityEventListener
    {
        private UnityEventBehaviour _behaviour;

        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;
        public event Action<bool> OnPause;
        public event Action OnQuit;

        public UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var go = new GameObject("UnityEventBehaviour");
            _behaviour = go.AddComponent<UnityEventBehaviour>();
            _behaviour.SetListener(this);
            return UniTask.CompletedTask;
        }

        public void Reset()
        {
            GameObject.Destroy(_behaviour.gameObject);
            _behaviour = null;
        }

        public void BroadcastOnUpdate() => OnUpdate.Broadcast();
        public void BroadcastOnLateUpdate() => OnLateUpdate.Broadcast();
        public void BroadcastOnFixedUpdate() => OnFixedUpdate.Broadcast();
        public void BroadcastOnPause(bool status) => OnPause.Broadcast(status);
        public void BroadcastOnQuit() => OnQuit.Broadcast();
    }
}
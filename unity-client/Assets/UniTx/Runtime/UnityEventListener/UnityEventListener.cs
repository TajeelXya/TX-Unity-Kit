using System;
using UniTx.Runtime.Extensions;
using UnityEngine;

namespace UniTx.Runtime.UnityEventListener
{
    public sealed class UnityEventListener : IUnityEventListener, IInitialisable, IResettable
    {
        private UnityEventBehaviour _behaviour;

        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;
        public event Action<bool> OnPause;
        public event Action OnQuit;

        public void Initialise()
        {
            var go = new GameObject("UnityEventBehaviour");
            _behaviour = go.AddComponent<UnityEventBehaviour>();
            _behaviour.SetListener(this);
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
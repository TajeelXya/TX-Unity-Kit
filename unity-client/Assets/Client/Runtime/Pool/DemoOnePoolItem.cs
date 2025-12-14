using UniTx.Runtime;
using UniTx.Runtime.Pool;
using UnityEngine;

namespace Client.Runtime.Pool
{
    public sealed class DemoOnePoolItem : MonoBehaviour, IPoolItem
    {
        private IPoolReturner _returner;

        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public void Initialise()
        {
            UniStatics.LogInfo("Initialise", this, Color.darkOliveGreen);
        }

        public void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.darkOliveGreen);
        }

        public void Return() => _returner.Return(this);

        public void SetPoolReturner(IPoolReturner returner) => _returner = returner;
    }
}

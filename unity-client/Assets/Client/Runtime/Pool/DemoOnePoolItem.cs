using UniTx.Runtime;
using UniTx.Runtime.Pool;
using UnityEngine;

namespace Client.Runtime.Pool
{
    public sealed class DemoOnePoolItem : MonoBehaviour, IPoolItem
    {
        private ISpawner _spawner;

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

        public void Return() => _spawner.Return(this);

        public void SetSpawner(ISpawner spawner) => _spawner = spawner;
    }
}

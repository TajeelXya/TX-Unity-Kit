using UnityEngine;
using UniTx.Runtime;
using UniTx.Runtime.Pool;

namespace Client.Runtime.Pool
{
    public sealed class DemoTwoPoolItem : MonoBehaviour, IPoolItem<IDemoTwoPoolItemData>
    {
        private ISpawner _spawner;

        public IDemoTwoPoolItemData Data { get; private set; }

        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public void Initialise()
        {
            UniStatics.LogInfo($"Initialise -> Message: {Data.Message}", this, Color.darkOliveGreen);
        }

        public void Reset()
        {
            UniStatics.LogInfo("Reset", this, Color.darkOliveGreen);
        }

        public void Return() => _spawner.Return(this);

        public void SetData(IPoolItemData data) => Data = (IDemoTwoPoolItemData)data;

        public void SetSpawner(ISpawner spawner) => _spawner = spawner;
    }
}
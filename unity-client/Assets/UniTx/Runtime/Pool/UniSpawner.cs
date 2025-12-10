using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Pool
{
    public sealed class UniSpawner : IInjectable, ISpawner
    {
        private readonly IDictionary<int, IPoolItem> _activeItems = new Dictionary<int, IPoolItem>();
        private IObjectPool<IPoolItem> _pool;
        private IResolver _resolver;

        public void Inject(IResolver resolver) => _resolver = resolver;

        public void Return(IPoolItem item)
        {
            _pool.Release(item);
            _activeItems.Remove(item.GameObject.GetInstanceID());
        }

        public void SetPool(IPoolItem prefab, Transform parent, int initialCapacity)
        {
            _pool = new ObjectPool<IPoolItem>
            (
                createFunc: CreateFunc(prefab, parent),
                actionOnRelease: itm => itm.Reset(),
                actionOnDestroy: itm => GameObject.Destroy(itm.GameObject),
                defaultCapacity: initialCapacity
            );
        }

        public void ClearSpawns()
        {
            while (_activeItems.Count > 0)
            {
                var enumerator = _activeItems.GetEnumerator();
                enumerator.MoveNext();
                enumerator.Current.Value.Return();
            }

            _pool.Clear();
            _activeItems.Clear();
        }

        public void Spawn(IPoolItemData data = null)
        {
            var item = _pool.Get();

            if (item is IPoolItemDataReceiver dataReceiver && data != null)
            {
                dataReceiver.SetData(data);
            }

            item.Initialise();
            _activeItems.Add(item.GameObject.GetInstanceID(), item);
        }

        private Func<IPoolItem> CreateFunc(IPoolItem prefab, Transform parent)
        {
            return () =>
            {
                var go = GameObject.Instantiate(prefab.GameObject, parent);
                var item = go.GetComponent<IPoolItem>();
                item.SetSpawner(this);

                if (item is IInjectable injectable)
                {
                    injectable.Inject(_resolver);
                }

                return item;
            };
        }
    }
}
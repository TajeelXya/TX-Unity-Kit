using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Pool
{
    public abstract class SpawnerBase : MonoBehaviour, IInjectable, ISpawner
    {
        protected IDictionary<int, IPoolItem> _activeItems;
        protected IObjectPool<IPoolItem> _pool;
        protected IResolver _resolver;

        public void Inject(IResolver resolver) => _resolver = resolver;

        public virtual void Initialise(IPoolItem prefab, Transform parent, int initialCapacity)
        {
            _pool = new ObjectPool<IPoolItem>
            (
                createFunc: CreateFunc(prefab, parent),
                actionOnRelease: itm => itm.Reset(),
                actionOnDestroy: itm => Destroy(itm.GameObject),
                defaultCapacity: initialCapacity
            );
            _activeItems = new Dictionary<int, IPoolItem>();
        }

        public virtual void ClearSpawns()
        {
            while (_activeItems.Count != 0)
            {
                _activeItems.First().Value.Return();
            }

            _pool.Clear();
            _activeItems.Clear();
        }

        public virtual void SpawnOne()
        {
            var item = _pool.Get();
            _activeItems.Add(item.GameObject.GetInstanceID(), item);
        }

        public virtual void Return(IPoolItem item)
        {
            _pool.Release(item);
            _activeItems.Remove(item.GameObject.GetInstanceID());
        }

        protected virtual Func<IPoolItem> CreateFunc(IPoolItem prefab, Transform parent)
        {
            return () =>
            {
                var go = Instantiate(prefab.GameObject, parent);
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
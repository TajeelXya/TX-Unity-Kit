namespace UniTx.Runtime.Pool
{
    public interface IPoolItem : ISceneEntity, IResettable
    {
        void SetSpawner(ISpawner spawner);

        void Return();
    }

    public interface IPoolItem<TData> : IPoolItem, IInitialisable<TData>
        where TData : IPoolItemData
    {
        TData Data { get; }
    }
}
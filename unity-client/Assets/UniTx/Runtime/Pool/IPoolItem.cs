namespace UniTx.Runtime.Pool
{
    public interface IPoolItem : ISceneEntity, IInitialisable, IResettable
    {
        void SetSpawner(ISpawner spawner);

        void Return();
    }

    public interface IPoolItem<TData> : IPoolItem, IPoolItemDataReceiver
        where TData : IPoolItemData
    {
        TData Data { get; }
    }
}
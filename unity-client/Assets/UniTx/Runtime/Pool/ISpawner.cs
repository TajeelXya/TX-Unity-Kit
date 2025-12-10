namespace UniTx.Runtime.Pool
{
    public interface ISpawner
    {
        void Return(IPoolItem item);
    }
}
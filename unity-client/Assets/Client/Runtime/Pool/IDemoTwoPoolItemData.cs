using UniTx.Runtime.Pool;

namespace Client.Runtime.Pool
{
    public interface IDemoTwoPoolItemData : IPoolItemData
    {
        string Message { get; }
    }
}
namespace UniTx.Runtime.Services
{
    /// <summary>
    /// Common service contract combining injectable, initialisable and resettable behaviors.
    /// </summary>
    public interface IService : IInitialisableAsync, IResettable
    {
        // Empty
    }
}

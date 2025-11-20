using Cysharp.Threading.Tasks;
using System.Threading;

namespace UniTx.Runtime
{
    /// <summary>
    /// Defines a synchronous initialisation capability.
    /// </summary>
    public interface IInitialisable
    {
        /// <summary>
        /// Initialises the object.
        /// </summary>
        void Initialise();
    }

    /// <summary>
    /// Defines a synchronous initialisation capability with a parameter.
    /// </summary>
    public interface IInitialisable<T>
    {
        /// <summary>
        /// Initialises the object using the given parameter.
        /// </summary>
        void Initialise(T param);
    }

    /// <summary>
    /// Defines an asynchronous initialisation capability.
    /// </summary>
    public interface IInitialisableAsync
    {
        /// <summary>
        /// Initialises the object asynchronously.
        /// </summary>
        UniTask InitialiseAsync(CancellationToken cToken = default);
    }

    /// <summary>
    /// Defines an asynchronous initialisation capability with a parameter.
    /// </summary>
    public interface IInitialisableAsync<T>
    {
        /// <summary>
        /// Initialises the object asynchronously using the given parameter.
        /// </summary>
        UniTask InitialiseAsync(T param, CancellationToken cToken = default);
    }
}
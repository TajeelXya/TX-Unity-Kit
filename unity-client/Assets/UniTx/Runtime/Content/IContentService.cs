using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UniTx.Runtime.Content
{
    /// <summary>
    /// Provides access to game data objects by key or type.
    /// </summary>
    public interface IContentService
    {
        /// <summary>
        /// Invoked when content has finished loading.
        /// </summary>
        event Action OnContentLoaded;

        /// <summary>
        /// Invoked when content has finished unloading.
        /// </summary>
        event Action OnContentUnloaded;

        /// <summary>
        /// Asynchronously loads all content associated with the given tags.
        /// </summary>
        /// <param name="tags">Tags used to determine which content to load.</param>
        /// <param name="cToken">Token to cancel the load operation.</param>
        UniTask LoadContentAsync(IEnumerable<string> tags, CancellationToken cToken = default);

        /// <summary>
        /// Asynchronously unloads all content associated with the given tags.
        /// </summary>
        /// <param name="tags">Tags used to determine which content to load.</param>
        /// <param name="cToken">Token to cancel the load operation.</param>
        UniTask UnloadContentAsync(IEnumerable<string> tags, CancellationToken cToken = default);

        /// <summary>
        /// Retrieves a single data object by its unique key.
        /// </summary>
        /// <typeparam name="T">
        /// The type of data object to retrieve. Must implement <see cref="IData"/>.
        /// </typeparam>
        /// <param name="key">
        /// The unique key identifying the data object.
        /// </param>
        /// <returns>
        /// The data object of type <typeparamref name="T"/> associated with the given key.
        /// </returns>
        T GetData<T>(string key)
            where T : IData;

        /// <summary>
        /// Retrieves multiple data objects corresponding to the provided keys.
        /// </summary>
        /// <typeparam name="T">
        /// The type of data objects to retrieve. Must implement <see cref="IData"/>.
        /// </typeparam>
        /// <param name="keys">
        /// A collection of keys identifying the data objects.
        /// </param>
        /// <returns>
        /// An enumerable collection of data objects of type <typeparamref name="T"/>.
        /// </returns>
        IEnumerable<T> GetData<T>(IEnumerable<string> keys)
            where T : IData;

        /// <summary>
        /// Retrieves all available data objects of the specified type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of data objects to retrieve. Must implement <see cref="IData"/>.
        /// </typeparam>
        /// <returns>
        /// An enumerable collection containing all data objects of type <typeparamref name="T"/>.
        /// </returns>
        IEnumerable<T> GetAllData<T>()
            where T : IData;
    }
}
using System.Collections.Generic;

namespace UniTx.Runtime.Content
{
    public static class ContentRegistry
    {
        private static readonly Dictionary<string, IDataLoader> _loaders = new();

        public static void Register<T>(string fileName)
            where T : IData
            => _loaders[fileName] = new DataLoader<T>();

        internal static IDataLoader GetLoader(string fileName)
            => _loaders.TryGetValue(fileName, out var loader) ? loader : null;
    }
}
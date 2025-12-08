using System;
using System.Collections.Generic;

namespace UniTx.Runtime.Services
{
    public static class ContentRegistry
    {
        private static readonly Dictionary<string, Type> _registry = new();

        public static void Clear() => _registry.Clear();

        public static void Register<T>(string fileName)
            where T : class
            => _registry[fileName] = typeof(T);

        public static Type GetCotentType(string fileName)
            => _registry.TryGetValue(fileName, out var array) ? array : null;
    }
}
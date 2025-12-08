using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Extensions;
using UniTx.Runtime.ResourceManagement;

namespace UniTx.Runtime.Services
{
    public sealed class ContentService : IService, IContentService
    {
        private readonly Dictionary<string, IData> _dataRegistry = new();

        public async UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var tags = new List<string> { "mg_content" };
            var files = await UniResources.LoadAssetGroupAsync<TextAsset>(tags);

            foreach (var file in files)
            {
                var fileName = file.name.FixTurkishChars();
                var type = ContentRegistry.GetCotentType(fileName);

                if (type == null)
                {
                    UniStatics.LogInfo($"File '{fileName}' not registered against any Type, skipping.", this, Color.red);
                    continue;
                }

                var wrapperType = typeof(JsonArray<>).MakeGenericType(type);
                var wrappedJson = $"{{ \"Items\": {file.text.FixTurkishChars()} }}";

                if (JsonUtility.FromJson(wrappedJson, wrapperType) is not { } wrapper) continue;

                var objs = (Array)wrapperType.GetField("Items").GetValue(wrapper);

                foreach (var obj in objs.OfType<IData>())
                {
                    if (!_dataRegistry.TryAdd(obj.Id, obj))
                    {
                        UniStatics.LogInfo(
                            $"Duplicate Id '{obj.Id}' found, conflicts with an already registered data. Please ensure all Ids are unique.",
                            this, Color.red);
                    }
                }
            }

            UniResources.DisposeAssetGroup(files);
        }

        public void Reset() => _dataRegistry.Clear();

        public T GetData<T>(string key)
            where T : IData
        {
            if (_dataRegistry.TryGetValue(key, out var asset) && asset is T typedAsset)
            {
                return typedAsset;
            }

            throw new KeyNotFoundException($"DataAsset with Id '{key}' not found.");
        }

        public IEnumerable<T> GetData<T>(IEnumerable<string> keys)
            where T : IData
            => keys == null ? Enumerable.Empty<T>() : keys.Select(GetData<T>);

        public IEnumerable<T> GetAllData<T>()
            where T : IData
            => _dataRegistry.Values.OfType<T>();
    }
}
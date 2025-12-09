using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Extensions;
using UniTx.Runtime.ResourceManagement;

namespace UniTx.Runtime.Content
{
    public sealed class ContentService : IContentService
    {
        private readonly IDictionary<string, IData> _dataRegistry = new Dictionary<string, IData>();

        public event Action OnContentLoaded;

        public event Action OnContentUnloaded;

        public async UniTask LoadContentAsync(IEnumerable<string> tags, CancellationToken cToken = default)
        {
            var files = await UniResources.LoadAssetGroupAsync<TextAsset>(tags);

            foreach (var file in files)
            {
                var objs = GetDataObjects(file);

                foreach (var obj in objs)
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
            OnContentLoaded.Broadcast();
        }

        public async UniTask UnloadContentAsync(IEnumerable<string> tags, CancellationToken cToken = default)
        {
            var files = await UniResources.LoadAssetGroupAsync<TextAsset>(tags);

            foreach (var file in files)
            {
                var objs = GetDataObjects(file);

                foreach (var obj in objs)
                {
                    if (!_dataRegistry.Remove(obj.Id))
                    {
                        UniStatics.LogInfo($"Attempting to remove un-registered data '{obj.Id}', skipping.", this, Color.red);
                    }
                }
            }

            UniResources.DisposeAssetGroup(files);
            OnContentUnloaded.Broadcast();
        }

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

        private IEnumerable<IData> GetDataObjects(TextAsset file)
        {
            var fileName = file.name.FixTurkishChars();
            var type = ContentRegistry.GetCotentType(fileName);

            if (type == null)
            {
                UniStatics.LogInfo($"File '{fileName}' not registered against any Type, skipping.", this, Color.yellow);
                return Enumerable.Empty<IData>();
            }

            var wrapperType = typeof(JsonArray<>).MakeGenericType(type);
            var wrappedJson = $"{{ \"Items\": {file.text.FixTurkishChars()} }}";

            return JsonUtility.FromJson(wrappedJson, wrapperType) is { } wrapper
                ? ((Array)wrapperType.GetField("Items").GetValue(wrapper)).OfType<IData>()
                : Enumerable.Empty<IData>();
        }
    }
}
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Services
{
    internal sealed class Serialiser : IInjectable, IResettable
    {
        private static string _dirPath = Path.Combine(Application.persistentDataPath, "Saves");
        private readonly Dictionary<string, ISavedData> _cache = new();
        private readonly Dictionary<string, ISavedData> _dirty = new();
        private IClock _clock;

        public void Inject(IResolver resolver) => _clock = resolver.Resolve<IClock>();

        public void Reset()
        {
            _dirty.Clear();
            _cache.Clear();
        }

        public void MarkDirty(ISavedData data) => _dirty.TryAdd(data.Id, data);

        public void SerialiseDirty()
        {
            if (_dirty.Count == 0) return;

            var data = _dirty.Values.ToArray();
            _dirty.Clear();

            SyncLocally(data);
            SyncToCloudAsync(data);
        }

        public T Deserialise<T>(string id) where T : ISavedData, new()
        {
            if (_cache.TryGetValue(id, out var data))
            {
                return (T)data;
            }

            var path = Path.Combine(_dirPath, $"{id}.json");
            var json = File.Exists(path) ? File.ReadAllText(path) : $"{{\"_id\": \"{id}\"}}";
            var obj = new T();
            JsonUtility.FromJsonOverwrite(json, obj);
            _cache[id] = obj;

            return obj;
        }

        private void SyncLocally(ISavedData[] data)
        {
            if (!Directory.Exists(_dirPath))
            {
                Directory.CreateDirectory(_dirPath);
            }

            foreach (var savedData in data)
            {
                savedData.ModifiedTimestamp = _clock.UnixTimestampNow;
                var json = JsonUtility.ToJson(savedData, true);
                var path = Path.Combine(_dirPath, $"{savedData.Id}.json");
                File.WriteAllText(path, json);
            }
        }

        private UniTask SyncToCloudAsync(ISavedData[] data, CancellationToken cToken = default)
        {
            // Empty Yet.
            return UniTask.CompletedTask;
        }

#if UNITY_EDITOR
        [MenuItem("UniTx/Open Saves")]
        public static void OpenSaves()
        {
            if (!Directory.Exists(_dirPath))
            {
                Directory.CreateDirectory(_dirPath);
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = _dirPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        [MenuItem("UniTx/Clear Saves")]
        public static void ClearSaves()
        {
            PlayerPrefs.DeleteAll();
            Directory.Delete(_dirPath, recursive: true);
        }
#endif
    }
}
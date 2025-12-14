using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Pool;

namespace UniTx.Runtime.Audio
{
    internal sealed class UniAudioService : IAudioService
    {
        private readonly UniSpawner _spawner = new();

        public UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var prefab = new GameObject("UniAudioSource").AddComponent<UniAudioSource>();
            _spawner.SetPool(prefab, UniStatics.Root.transform, 5);
            return UniTask.CompletedTask;
        }

        public void Play2D(IAudioConfig config)
        {
            config.Data.SpatialBlend = 0f;
            var data = (UniAudioConfigData)config.Data;
            _spawner.Spawn(data);
        }

        public void Play3D(IAudioConfig config, Vector3 position)
        {
            config.Data.SpatialBlend = 1f;
            var data = (UniAudioConfigData)config.Data;
            var source = _spawner.Spawn(data);
        }

        public void PlayAttached(IAudioConfig config, Transform parent)
        {
            throw new System.NotImplementedException();
        }

        public void PlayMusic(IAudioConfig config)
        {
            throw new System.NotImplementedException();
        }
    }
}
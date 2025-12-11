using UnityEngine;
using UniTx.Runtime.Pool;

namespace UniTx.Runtime.Audio
{
    internal sealed class UniAudioService : IAudioService, IInitialisable, IResettable
    {
        private readonly UniSpawner _spawner = new();

        private UniAudioSource _prefab;

        public void Initialise()
        {
            _prefab = new GameObject("UniAudioSource").AddComponent<UniAudioSource>();
            _spawner.SetPool(_prefab, UniStatics.Root.transform, 5);
        }

        public void Reset()
        {
            _spawner.ClearSpawns();
            Object.Destroy(_prefab.gameObject);
        }

        public void Play2D(IAudioConfig config)
        {
            throw new System.NotImplementedException();
        }

        public void Play3D(IAudioConfig config, Vector3 position)
        {
            throw new System.NotImplementedException();
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
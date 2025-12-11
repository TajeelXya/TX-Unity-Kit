using UnityEngine;
using UniTx.Runtime.Pool;

namespace UniTx.Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class UniAudioSource : MonoBehaviour, IPoolItem<UniAudioConfigData>
    {
        private AudioSource _source;
        private ISpawner _spawner;

        public UniAudioConfigData Data { get; private set; }

        public GameObject GameObject => gameObject;

        public Transform Transform => transform;

        public void Initialise()
        {
            _source.clip = Data.Clip;
            _source.volume = Data.Volume;
            _source.pitch = Data.Pitch;
            _source.loop = Data.Loop;
            _source.minDistance = Data.MinDistance;
            _source.maxDistance = Data.MaxDistance;
            _source.outputAudioMixerGroup = Data.MixerGroup;
            _source.mute = false;
            _source.Play();
        }

        public void Reset()
        {
            _source.mute = true;
            _source.Stop();
        }

        public void Return() => _spawner.Return(this);

        public void SetData(IPoolItemData data) => Data = (UniAudioConfigData)data;

        public void SetSpawner(ISpawner spawner) => _spawner = spawner;

        private void Awake() => _source = GetComponent<AudioSource>();
    }
}
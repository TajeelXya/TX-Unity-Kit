using UnityEngine;
using UnityEngine.Audio;

namespace UniTx.Runtime.Audio
{
    [CreateAssetMenu(fileName = "NewAudioConfig", menuName = "UniTx/Audio/Config")]
    public sealed class UniAudioConfig : ScriptableObject, IAudioConfig
    {
        [Header("Clip")]
        [SerializeField] private AudioClip _clip;

        [Header("Settings")]
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(-3f, 3f)] private float _pitch = 1f;

        [Header("3D Settings")]
        [SerializeField, Range(0f, 1f)] private float _spatialBlend = 0f;
        [SerializeField] private float _minDistance = 1f;
        [SerializeField] private float _maxDistance = 20f;
        [SerializeField] private bool _loop;

        [Header("Mixer")]
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public AudioClip Clip => _clip;

        public float Volume => _volume;

        public float Pitch => _pitch;

        public float SpatialBlend => _spatialBlend;

        public float MinDistance => _minDistance;

        public float MaxDistance => _maxDistance;

        public bool Loop => _loop;

        public AudioMixerGroup MixerGroup => _mixerGroup;

        public void Play2D() => UniAudio.Play2D(this);

        public void Play3D(Vector3 position) => UniAudio.Play3D(this, position);

        public void PlayAttached(Transform parent) => UniAudio.PlayAttached(this, parent);
    }
}
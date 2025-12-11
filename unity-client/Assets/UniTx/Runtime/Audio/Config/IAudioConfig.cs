using UnityEngine;
using UnityEngine.Audio;

namespace UniTx.Runtime.Audio
{
    public interface IAudioConfig
    {
        AudioClip Clip { get; }

        float Volume { get; }

        float Pitch { get; }

        float SpatialBlend { get; }

        float MinDistance { get; }

        float MaxDistance { get; }

        bool Loop { get; }

        AudioMixerGroup MixerGroup { get; }

        void Play2D();

        void Play3D(Vector3 position);

        void PlayAttached(Transform parent);
    }
}
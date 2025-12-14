using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniTx.Runtime.Audio;
using UniTx.Runtime.Bootstrap;
using UnityEngine;

public sealed class AudioStep : LoadingStepBase
{
    [SerializeField] private ScriptableObject _musicSo;
    [SerializeField] private ScriptableObject _demoSo;

    public async override UniTask InitialiseAsync(CancellationToken cToken = default)
    {
        if (_musicSo is IAudioConfig musicConfig)
        {
            UniAudio.PlayMusic(musicConfig);
        }

        if (_demoSo is IAudioConfig demoConfig)
        {
            UniAudio.Play2D(demoConfig);

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);

            UniAudio.Play3D(demoConfig, Vector3.forward);

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            UniAudio.Play2D(demoConfig);
            UniAudio.Play2D(demoConfig);
            UniAudio.Play2D(demoConfig);
            UniAudio.Play2D(demoConfig);
            UniAudio.Play2D(demoConfig);
            UniAudio.Play2D(demoConfig);
        }
    }
}

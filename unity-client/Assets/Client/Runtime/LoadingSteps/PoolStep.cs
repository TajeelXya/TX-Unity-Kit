using Client.Runtime.Pool;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.IoC;
using UniTx.Runtime.Pool;
using System;

namespace Client.Runtime
{
    public sealed class PoolStep : LoadingStepBase, IInjectable
    {
        [SerializeField] private DemoOnePoolItem _itemOne;
        [SerializeField] private DemoTwoPoolItem _itemTwo;

        private readonly UniSpawner _spawnerOne = new();
        private readonly UniSpawner _spawnerTwo = new();

        public void Inject(IResolver resolver)
        {
            _spawnerOne.Inject(resolver);
            _spawnerTwo.Inject(resolver);
        }

        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            _spawnerOne.SetPool(_itemOne, null, 1);
            _spawnerTwo.SetPool(_itemTwo, null, 1);

            _spawnerOne.Spawn();
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            _spawnerOne.Spawn();
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            _spawnerTwo.Spawn(new DemoTwoPoolItemData("Hello from UniTx Pool 1."));
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            _spawnerTwo.Spawn(new DemoTwoPoolItemData("Hello from UniTx Pool 2."));
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            _spawnerOne.ClearSpawns();
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cToken);
            _spawnerTwo.ClearSpawns();
        }
    }
}
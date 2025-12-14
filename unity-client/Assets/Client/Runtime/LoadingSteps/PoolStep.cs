using Client.Runtime.Pool;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.Pool;
using System;

namespace Client.Runtime
{
    public sealed class PoolStep : LoadingStepBase
    {
        [SerializeField] private DemoOnePoolItem _itemOne;
        [SerializeField] private DemoTwoPoolItem _itemTwo;

        private readonly UniSpawner _spawnerOne = new();
        private readonly UniSpawner _spawnerTwo = new();

        public override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            _spawnerOne.SetPool(_itemOne, null, 1);
            _spawnerTwo.SetPool(_itemTwo, null, 1);

            _spawnerOne.Spawn();
            _spawnerOne.Spawn();
            _spawnerTwo.Spawn(new DemoTwoPoolItemData("Hello from UniTx Pool 1."));
            _spawnerTwo.Spawn(new DemoTwoPoolItemData("Hello from UniTx Pool 2."));
            _spawnerOne.ClearSpawns();
            _spawnerTwo.ClearSpawns();

            return UniTask.CompletedTask;
        }
    }
}
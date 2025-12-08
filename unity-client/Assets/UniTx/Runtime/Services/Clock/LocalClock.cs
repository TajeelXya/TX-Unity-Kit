using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniTx.Runtime.Extensions;

namespace UniTx.Runtime.Services
{
    public sealed class LocalClock : IService, IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public long UnixTimestampNow => UtcNow.ToUnixTimestamp();

        public UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            return UniTask.CompletedTask;
        }

        public void Reset()
        {
            // Empty
        }
    }
}
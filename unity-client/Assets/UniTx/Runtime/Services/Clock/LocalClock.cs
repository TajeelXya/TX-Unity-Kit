using System;
using UniTx.Runtime.Extensions;

namespace UniTx.Runtime.Services
{
    public sealed class LocalClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public long UnixTimestampNow => UtcNow.ToUnixTimestamp();
    }
}
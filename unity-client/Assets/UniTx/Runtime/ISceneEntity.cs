using UnityEngine;

namespace UniTx.Runtime
{
    public interface ISceneEntity
    {
        GameObject GameObject { get; }

        Transform Transform { get; }
    }
}
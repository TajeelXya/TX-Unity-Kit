using System;
using UnityEngine;

namespace UniTx.Runtime
{
    [CreateAssetMenu(fileName = "NewUniTxConfig", menuName = "UniTx/Config")]
    [Serializable]
    internal sealed class UniTxConfig : ScriptableObject
    {
        [SerializeField] private string _widgetsAssetDataKey = default;
        [SerializeField] private string _widgetsParentTag = default;

        public string WidgetsAssetDataKey => _widgetsAssetDataKey;
        public string WidgetsParentTag => _widgetsParentTag;
    }
}
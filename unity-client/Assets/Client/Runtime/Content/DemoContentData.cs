using System;
using UnityEngine;

namespace Client.Runtime.Content
{
    [Serializable]
    public sealed class DemoContentData : IDemoContentData
    {
        [SerializeField] private string _id = default;
        [SerializeField] private string _name = default;
        [SerializeField] private string[] _descriptions = default;

        public string Id => _id;
        public string Name => _name;
        public string[] Descriptions => _descriptions;
    }
}


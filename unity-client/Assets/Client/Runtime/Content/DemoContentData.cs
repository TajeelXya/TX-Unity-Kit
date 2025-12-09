using System;
using UniTx.Runtime.Content;
using UnityEngine;

[Serializable]
public sealed class DemoContentData : IData
{
    [SerializeField] private string _id = default;
    [SerializeField] private string _name = default;
    [SerializeField] private string[] _descriptions = default;

    public string Id => _id;
    public string Name => _name;
    public string[] Descriptions => _descriptions;
}

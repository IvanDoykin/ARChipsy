using System;
using UnityEngine;

[Serializable]
public class ARPrefabInfo
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;

    public string Id => _id;
    public GameObject Prefab => _prefab;
}

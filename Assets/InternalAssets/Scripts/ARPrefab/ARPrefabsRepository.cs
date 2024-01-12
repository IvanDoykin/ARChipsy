using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ARPrefabsRepository", menuName = "ARData/ARPrefabsRepository", order = 2)]
public class ARPrefabsRepository : ScriptableObject
{
    [SerializeField] private List<ARPrefabInfo> _arPrefabs = new List<ARPrefabInfo>();
    [SerializeField] private GameObject _defaultPrefab;

    public GameObject GetByName(string name)
    {
        GameObject prefab;

        if (name != null && FilesIO.TryGetMarkerIdByName(name, out string id))
        {
            prefab = _arPrefabs.FirstOrDefault(x => x.Id == id).Prefab;
        }
        else
        {
            prefab = _defaultPrefab;
        }

        return prefab;
    }
}
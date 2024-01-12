using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedContent : MonoBehaviour
{
    private const float _delayForInitialize = 0.2f;

    [SerializeField] private ARPrefabsRepository _repository;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_delayForInitialize);
        LoadWithName(GetComponent<ARTrackedImage>().referenceImage.name);
    }

    private void LoadWithName(string name)
    {
        Debug.Log($"Load with name {name}.");
        var content = Instantiate(_repository.GetByName(name), transform);
    }
}

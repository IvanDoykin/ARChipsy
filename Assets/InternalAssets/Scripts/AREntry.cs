using UnityEngine;

public class AREntry : MonoBehaviour
{
    [SerializeField] private ContentLoader _loader;

    private void Start()
    {
        _loader.Initialize();
    }
}

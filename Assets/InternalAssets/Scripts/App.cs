using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private ContentLoader _loader;

    private void Start()
    {
        _loader.Initialize();
    }

    public void OpenAR()
    {

    }
}

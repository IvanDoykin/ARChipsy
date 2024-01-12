using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackingLogger : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _trackedImageManager;
    [SerializeField] private TextMeshProUGUI _log;

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnChanged; 
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            _log.text = newImage.referenceImage.name;
        }
    }
}

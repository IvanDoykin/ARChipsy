using GLTFast.Schema;
using System.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkersLoader : MonoBehaviour
{
    private const float _markerSize = 0.25f;

    [SerializeField] private ContentLoader _contentLoader;
    [SerializeField] private ARTrackedImageManager _trackedImageManager;
    [SerializeField] private XRReferenceImageLibrary _imagesReference;

    private bool _initialized = false;

    private void Awake()
    {
        ARSession.stateChanged += (ARSessionStateChangedEventArgs state) =>
        {
            Debug.Log($"ARSession state: {state.state}.");
            if (!_initialized && (state.state == ARSessionState.SessionTracking || state.state == ARSessionState.SessionInitializing))
            {
                _initialized = true;
                var mutableLibrary = _trackedImageManager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;
                _trackedImageManager.subsystem.imageLibrary = mutableLibrary;
                _trackedImageManager.enabled = true;

                StartCoroutine(LoadMarkers());
            }
        };
    }

    private IEnumerator LoadMarkers()
    {
        foreach (var markerPath in FilesIO.MarkerPaths)
        {
            yield return StartCoroutine(AddImage(LoadImageByPath(markerPath), FilesIO.GetFilenameByPath(markerPath)));
        }
    }

    private Texture2D LoadImageByPath(string path)
    {
        Texture2D MakeReadable(Texture2D source)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Texture2D readableTexture = new Texture2D(source.width, source.height);
            readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            readableTexture.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);
            return readableTexture;
        }

        Texture2D image = null;
        byte[] imageRaw;

        if (System.IO.File.Exists(path))
        {
            imageRaw = System.IO.File.ReadAllBytes(path);
            image = new Texture2D(2, 2);
            image.LoadImage(imageRaw);
            image = MakeReadable(image);
        }

        return image;
    }

    private IEnumerator AddImage(Texture2D imageToAdd, string imageName)
    {
        if (_trackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            var job = mutableLibrary.ScheduleAddImageWithValidationJob(imageToAdd, imageName, _markerSize);
            yield return new WaitUntil(() => job.jobHandle.IsCompleted);
            Debug.Log($"Job status: {job.status}.");
            Debug.Log($"Tracking subsistem check: {_trackedImageManager.subsystem.running}.");
        }
    }
}

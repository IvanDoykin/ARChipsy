using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ContentDownloader : MonoBehaviour, IConnection
{
    public event Action<ConnectionInfo> HasReceivedInfo;
    public event Action HasDownloadedContent;
    public event Action HasFailedDownload;

    public bool IsDownloaded(ContentInfo content)
    {
        Debug.Log($"Check on download: {FilesIO.GetContentPath(content)}.");
        return File.Exists(FilesIO.GetContentPath(content));
    }

    public void Download(ContentInfo content)
    {
        StartCoroutine(DownloadContent(content));
    }

    private IEnumerator DownloadContent(ContentInfo content)
    {
        string url = content.Link;

        string contentSavePath = FilesIO.GetContentDirectoryPath(content);
        if (!Directory.Exists(Path.GetDirectoryName(contentSavePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(contentSavePath));
        }
        contentSavePath = FilesIO.GetContentPath(content);

        var request = new UnityWebRequest(url);
        request.method = UnityWebRequest.kHttpVerbGET;

        var downloadHandler = new DownloadHandlerFile(contentSavePath);
        downloadHandler.removeFileOnAbort = true;

        request.downloadHandler = downloadHandler;

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            HasReceivedInfo?.Invoke(new ConnectionInfo($"Connection has refused. Please, relaunch app.", ConnectionStatus.Failure));
            request.Abort();
            HasFailedDownload?.Invoke();
            yield break;
        }
        else
        {
            HasReceivedInfo?.Invoke(new ConnectionInfo("Connection to server, please wait...", ConnectionStatus.Success));
        }

        while (!request.isDone)
        {
            if (request.result == UnityWebRequest.Result.InProgress)
            {
                HasReceivedInfo?.Invoke(new ConnectionInfo($"Downloading file: {content.Name}[{(request.downloadProgress * 100).ToString("0.0")}]", ConnectionStatus.Success));
            }
            yield return null;
        }

        HasReceivedInfo?.Invoke(new ConnectionInfo($"All done.", ConnectionStatus.Stop));
        HasDownloadedContent?.Invoke();
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class ContentLoader : MonoBehaviour
{
    public UnityEvent HasInitialized;

    [SerializeField] private ContentRepository _repository;
    [SerializeField] private ContentDownloader _downloader;

    private int _currentContentIndex = 0;
    private int _lastContentIndex = 0;

    public void Initialize()
    {
        _downloader.HasDownloadedContent += DownloadNextContent;
        _downloader.HasFailedDownload += Music.Instance.PlayFail;

        _lastContentIndex = _repository.Content.Count - 1;
        DownloadNextContent();
    }

    private void DownloadNextContent()
    {
        if (_currentContentIndex <= _lastContentIndex)
        {
            DownloadCurrentContent(_repository.Content[_currentContentIndex]);
        }
        else
        {
            FilesIO.CachePaths();
            HasInitialized?.Invoke();
        }
    }

    private void DownloadCurrentContent(ContentInfo content)
    {
        _currentContentIndex++;

        if (!_downloader.IsDownloaded(content))
        {
            _downloader.Download(content);
        }
        else
        {
            DownloadNextContent();
        }
    }
}

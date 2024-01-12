using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class FilesIO
{
    private const string _idPattern = "id\\(.*?\\)";

    private static List<string> _markerPaths = new List<string>();
    private static List<string> _contentPaths = new List<string>();

    public static IReadOnlyList<string> MarkerPaths => _markerPaths;

    public static string GetContentDirectoryPath(ContentInfo content)
    {
        return Path.Combine(Application.persistentDataPath, content.Type.ToString());
    }

    public static string GetContentPath(ContentInfo content)
    {
        return Path.Combine(Application.persistentDataPath, content.Type.ToString(), $"id({content.Id})" + content.Name);
    }

    public static string GetFilenameByPath(string path)
    {
        return Path.GetFileName(path);
    }

    public static void CachePaths()
    {
        _markerPaths = new List<string>();
        _contentPaths = new List<string>();

        foreach (var file in Directory.GetFiles(Path.Combine(Application.persistentDataPath, ContentType.Marker.ToString())))
        {
            _markerPaths.Add(file);
        }
        foreach (var file in Directory.GetFiles(Path.Combine(Application.persistentDataPath, ContentType.Content.ToString())))
        {
            _contentPaths.Add(file);
        }
    }

    public static string GetContentPathById(string id)
    {
        foreach (var content in _contentPaths)
        {
            if (content.Contains($"id({id})"))
            {
                return content;    
            }
        }

        throw new Exception($"Can't find content with id({id}).");
    }

    public static bool TryGetMarkerIdByName(string name, out string id)
    {
        foreach (var marker in _markerPaths)
        {
            if (marker.Contains(name))
            {
                id = Regex.Match(name, _idPattern).Value;
                id = id.Substring(3, id.Length - 4);
                Debug.Log(id);
                return true;
            }
        }

        id = string.Empty;
        return false;
    }
}

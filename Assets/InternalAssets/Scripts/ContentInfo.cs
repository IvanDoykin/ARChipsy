using System;
using UnityEngine;

public enum ContentType
{
    Marker,
    Content
}

[Serializable]
public class ContentInfo
{
    [SerializeField] private string _name;
    [SerializeField] private ContentType _type;
    [SerializeField] private string _link;
    [SerializeField] private string _id;

    public string Name => _name;
    public ContentType Type => _type;
    public string Link => _link;
    public string Id => _id;

    public ContentInfo(string name, ContentType type, string link, string id)
    {
        _name = name;
        _type = type;
        _link = link;
        _id = id;
    }
}

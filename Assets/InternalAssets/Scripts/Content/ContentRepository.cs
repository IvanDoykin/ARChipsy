using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentRepository", menuName = "ARData/ContentRepository", order = 1)]
public class ContentRepository : ScriptableObject
{
    [SerializeField] private List<ContentInfo> _content = new List<ContentInfo>();
    public IReadOnlyList<ContentInfo> Content => _content;
}
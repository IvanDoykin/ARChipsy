public struct Content
{
    private object _rawData;
    private ContentInfo _info;

    public object RawData => _rawData;
    public ContentInfo Info => _info;

    public Content(object rawData, ContentInfo info)
    {
        _rawData = rawData;
        _info = info;
    }
}

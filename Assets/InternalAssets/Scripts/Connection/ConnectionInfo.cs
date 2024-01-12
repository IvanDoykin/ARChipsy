public struct ConnectionInfo
{
    private string _description;
    private ConnectionStatus _connectionStatus;

    public string Description => _description;
    public ConnectionStatus ConnectionStatus => _connectionStatus;

    public ConnectionInfo(string description, ConnectionStatus connectionStatus)
    {
        _description = description;
        _connectionStatus = connectionStatus;
    }
}

using System;

public interface IConnection
{
    public event Action<ConnectionInfo> HasReceivedInfo;
}

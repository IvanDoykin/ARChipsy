using TMPro;
using UnityEngine;

public class ConnectionView : MonoBehaviour
{
    private const string _chipsCracked = "Cracked";

    [SerializeField] private Animator _chips;
    [SerializeField] private TextMeshProUGUI _description;
    private IConnection _connection;

    private void Start()
    {
        _connection = GetComponentInParent<IConnection>();
        _connection.HasReceivedInfo += ViewConnection;
    }

    private void OnDestroy()
    {
        _connection.HasReceivedInfo -= ViewConnection;
    }

    private void ViewConnection(ConnectionInfo connectionInfo)
    {
        _description.text = connectionInfo.Description;
        if (connectionInfo.ConnectionStatus == ConnectionStatus.Failure)
        {
            _chips.SetTrigger(_chipsCracked);
        }
    }
}

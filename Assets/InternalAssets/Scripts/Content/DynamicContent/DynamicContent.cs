using UnityEngine;

public abstract class DynamicContent : MonoBehaviour
{
    [SerializeField] protected string _id;

    public async virtual void Load(string path) { }
}
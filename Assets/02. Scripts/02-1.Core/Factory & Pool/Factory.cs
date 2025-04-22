using UnityEngine;

public abstract class Factory<T> : MonoBehaviour where T : MonoBehaviour
{
    public abstract T GetProduct(GameObject productGO, Vector3 position);
}

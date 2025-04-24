using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _yOffset = 10f;

    private void LateUpdate()
    {
        Vector3 newPosition = _target.position;
        newPosition.y += _yOffset;
        transform.position = newPosition;
    }
}

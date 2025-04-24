using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _yOffset = 10f;
    
    private void LateUpdate()
    {
        SetNewPosition();
        SetNewEulerAngles();
    }

    private void SetNewPosition()
    {
        Vector3 newPosition = _target.position;
        newPosition.y += _yOffset;
        transform.position = newPosition;
    }

    private void SetNewEulerAngles()
    {
        Vector3 newEulerAngles = _target.eulerAngles;
        newEulerAngles.x = 90f;
        newEulerAngles.z = 0f;
        transform.eulerAngles = newEulerAngles;
    }

}

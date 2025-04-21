using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;
    private void Awake()
    {
        
    }

    private void Update()
    {
        // 보간, smoothing 기법
        transform.position = _targetTransform.position;
    }
}

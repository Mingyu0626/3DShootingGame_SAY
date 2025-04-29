using UnityEngine;
using System.Collections.Generic;
public enum CameraMode
{
    FPS, // default
    TPS,
    Quarter
}
public class CameraController : MonoBehaviour
{
    private ICameraState _fpsState, _tpsState, _quarterState;
    public ICameraState FpsState { get => _fpsState; set => _fpsState = value; }
    public ICameraState TpsState { get => _tpsState; set => _tpsState = value; }
    public ICameraState QuarterState { get => _quarterState; set => _quarterState = value; }

    private CameraMode _currentCameraMode;
    public CameraMode CurrentCameraMode { get => _currentCameraMode; set => _currentCameraMode = value; }

    [SerializeField]
    private List<Transform> _targetTransforms = new List<Transform>();
    public List<Transform> TargetTransforms { get => _targetTransforms; set => _targetTransforms = value; }

    private Transform _currentTargetTransform;
    public Transform CurrentTargetTransform { get => _currentTargetTransform; set => _currentTargetTransform = value; }

    private CameraStateContext _cameraStateContext;
    public CameraStateContext CameraStateContext { get => _cameraStateContext; set => _cameraStateContext = value; }

    [SerializeField]
    private float _rotationSpeed; // 카메라 회전 속도
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
    
    private float _rotationX = 0;
    private float _rotationY = 0;
    public float RotationX { get => _rotationX; set => _rotationX = value; }
    public float RotationY { get => _rotationY; set => _rotationY = value; }

    private void Awake()
    {
        LockMouseCursor();
    }
    private void Start()
    {
        _fpsState = new CameraFPSState();
        _tpsState = new CameraTPSState();
        _quarterState = new CameraQuarterState();
        _cameraStateContext = new CameraStateContext(this);
        _cameraStateContext.ChangeState(_fpsState);
    }
    private void LateUpdate()
    {
        FollowTarget();
        _cameraStateContext.CurrentState.Update();
    }
    private void LockMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void ConfineMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    private void FollowTarget()
    {
        transform.position = _currentTargetTransform.position;
    }
}

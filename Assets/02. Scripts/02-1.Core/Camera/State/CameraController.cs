using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private Dictionary<ECameraMode, ICameraState> _cameraStateDict;
    public Dictionary<ECameraMode, ICameraState> CameraStateDict 
    { get => _cameraStateDict; set => _cameraStateDict = value; }

    private CameraStateContext _cameraStateContext;
    public CameraStateContext CameraStateContext { get => _cameraStateContext; set => _cameraStateContext = value; }

    private ECameraMode _currentCameraMode;
    public ECameraMode CurrentCameraMode { get => _currentCameraMode; set => _currentCameraMode = value; }

    [SerializeField]
    private List<Transform> _targetTransforms = new List<Transform>();
    public List<Transform> TargetTransforms { get => _targetTransforms; set => _targetTransforms = value; }

    private Transform _currentTargetTransform;
    public Transform CurrentTargetTransform { get => _currentTargetTransform; set => _currentTargetTransform = value; }

    [SerializeField]
    private UI_Weapon _uiWeapon;
    public UI_Weapon UiWeapon { get => _uiWeapon; }

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
        _cameraStateDict = new Dictionary<ECameraMode, ICameraState>
        { 
            {ECameraMode.FPS, new CameraFPSState() },
            {ECameraMode.TPS, new CameraTPSState() },
            {ECameraMode.Quarter, new CameraQuarterState() }
        };
        _cameraStateContext = new CameraStateContext(this);
        _cameraStateContext.ChangeState(_cameraStateDict[ECameraMode.FPS]);
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
    private void FollowTarget()
    {
        transform.position = _currentTargetTransform.position;
    }
}

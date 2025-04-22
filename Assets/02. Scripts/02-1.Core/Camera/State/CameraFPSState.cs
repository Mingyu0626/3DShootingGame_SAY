using UnityEngine;

public class CameraFPSState : ICameraState, IRotate
{
    private CameraController _cameraController;
    private float _rotationYPositiveLimit = 40f;
    private float _rotationYNegativeLimit = -40f;
    public void Enter(CameraController playerController)
    {
        _cameraController = playerController;
        _cameraController.CurrentCameraMode = CameraMode.FPS;
        _cameraController.CurrentTargetTransform = _cameraController.TargetTransforms[(int)CameraMode.FPS];
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.TpsState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.QuarterState);
        }
        Rotate();
    }
    public void Exit()
    {
    }
    public void Rotate()
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 2. 회전한 양만큼 누적시켜 나간다.
        _cameraController.RotationX += mouseX * _cameraController.RotationSpeed * Time.deltaTime;
        _cameraController.RotationY += mouseY * _cameraController.RotationSpeed * Time.deltaTime;
        ClampYRotation();

        // 3. 회전 방향으로 회전시킨다.
        _cameraController.transform.eulerAngles = 
            new Vector3(-_cameraController.RotationY, _cameraController.RotationX + 90f, 0f);
    }
    private void ClampYRotation()
    {
        _cameraController.RotationY 
            = Mathf.Clamp(_cameraController.RotationY, _rotationYNegativeLimit, _rotationYPositiveLimit);
    }
}

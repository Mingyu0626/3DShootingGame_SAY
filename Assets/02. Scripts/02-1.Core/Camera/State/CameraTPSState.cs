using UnityEngine;

public class CameraTPSState : ICameraState, IRotate
{
    private CameraController _cameraController;
    private float _rotationYPositiveLimit = 10f;
    private float _rotationYNegativeLimit = -5f;
    public void Enter(CameraController cameraController)
    {
        _cameraController = cameraController;
        _cameraController.CurrentCameraMode = ECameraMode.TPS;
        _cameraController.CurrentTargetTransform = _cameraController.TargetTransforms[(int)ECameraMode.TPS];
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.CameraStateDict[ECameraMode.FPS]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.CameraStateDict[ECameraMode.Quarter]);
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
        _cameraController.RotationY = 
            Mathf.Clamp(_cameraController.RotationY, _rotationYNegativeLimit, _rotationYPositiveLimit);
    }
}

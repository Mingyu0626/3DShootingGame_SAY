using TMPro.Examples;
using UnityEngine;

public class CameraQuarterState : ICameraState, IRotate
{
    private CameraController _cameraController;

    public void Enter(CameraController cameraController)
    {
        _cameraController = cameraController;
        _cameraController.CurrentCameraMode = ECameraMode.Quarter;
        _cameraController.CurrentTargetTransform = _cameraController.TargetTransforms[(int)ECameraMode.Quarter];
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.FpsState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.TpsState);
        }
        Rotate();
    }
    public void Exit()
    {

    }

    public void Rotate()
    {
        _cameraController.transform.LookAt(_cameraController.CurrentTargetTransform.parent);
    }
}

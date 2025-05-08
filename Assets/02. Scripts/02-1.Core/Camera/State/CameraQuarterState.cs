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
        _cameraController.UiWeapon.UiCrossHair.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.CameraStateDict[ECameraMode.FPS]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _cameraController.CameraStateContext.ChangeState(_cameraController.CameraStateDict[ECameraMode.TPS]);
        }
        Rotate();
    }
    public void Exit()
    {
        _cameraController.UiWeapon.UiCrossHair.SetActive(true);
    }

    public void Rotate()
    {
        _cameraController.transform.LookAt(_cameraController.CurrentTargetTransform.parent);
    }
}

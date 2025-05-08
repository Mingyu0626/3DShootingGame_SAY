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

        // 쿼터뷰에서 카메라 컨트롤러와 플레이어가 회전하므로,
        // 플레이어 방향과 일치하는 해당 회전 값을 다른 상태에서도 유지시켜주기 위함이다.
        _cameraController.RotationX = _cameraController.transform.eulerAngles.y;
        _cameraController.RotationY = 0f;
    }

    public void Rotate()
    {
        _cameraController.transform.LookAt(_cameraController.CurrentTargetTransform.parent);
    }
}

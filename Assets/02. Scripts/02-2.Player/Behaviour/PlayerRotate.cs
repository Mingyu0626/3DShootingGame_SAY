using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed; // 플레이어의 회전 속도, 플레이어의 회전 속도는 카메라 회전 속도와 동일해야한다.
    private float _rotationX = 0;

    [SerializeField]
    private CameraController _cameraController;
    private void Awake()
    {
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        if (_cameraController.CurrentCameraMode == CameraMode.Quarter)
        {
            return;
        }
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        // 2. 회전한 양만큼 누적시켜 나간다.
        _rotationX += mouseX * _rotationSpeed * Time.deltaTime;
        // 3. 회전 방향으로 회전시킨다.
        transform.eulerAngles = new Vector3(0f, _rotationX, 0f);
    }
}

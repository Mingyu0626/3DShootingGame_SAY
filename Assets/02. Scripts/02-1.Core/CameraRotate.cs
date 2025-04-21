using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // 카메라 회전 스크립트
    // 목표 : 마우스를 조작하면 카메라를 그 방향으로 회전시키고 싶다.
    [SerializeField]
    private float _rotationSpeed; // 카메라 회전 속도

    // 카메라 각도는 0도에서부터 시작한다고 기준을 세운다.
    private float _rotationX = 0;
    private float _rotationY = 0;
    private void Awake()
    {
        LockMouseCursor();
    }
    private void Update()
    {
        Rotate();
    }
    private void LockMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Rotate()
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 2. 회전한 양만큼 누적시켜 나간다.
        _rotationX += mouseX * _rotationSpeed * Time.deltaTime;
        _rotationY += mouseY * _rotationSpeed * Time.deltaTime;
        ClampYRotation();

        // 3. 회전 방향으로 회전시킨다.
        transform.eulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
    }
    private void ClampYRotation()
    {
        _rotationY = Mathf.Clamp(_rotationY, -40f, 40f);
    }
}

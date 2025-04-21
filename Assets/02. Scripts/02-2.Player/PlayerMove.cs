using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목표 : WASD를 누르면, 캐릭터를 카메라 방향에 맞게 이동시키고 싶다.
    [SerializeField]
    private float _moveSpeed;
    private CharacterController _characterController;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        
        // 메인 카메라를 기준으로 방향을 변환한다.
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        // TransformDirection : 로컬 공간의 벡터를 월드 공간의 벡터로 바꿔주는 함수
        // transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }
}

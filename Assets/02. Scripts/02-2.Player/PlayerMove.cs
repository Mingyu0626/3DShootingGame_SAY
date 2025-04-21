using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerData _playerData;
    private CharacterController _characterController;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
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
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 메인 카메라를 기준으로 방향을 변환한다.
        // TransformDirection : 로컬 공간의 벡터를 월드 공간의 벡터로 바꿔주는 함수

        if (_playerData.IsClimbing)
        {
            moveDirection = new Vector3(horizontal, 1f, 0f).normalized;
            _playerData.YVelocity = 0f;
        }
        else
        {
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            // 중력 적용
            moveDirection = ApplyGravity(moveDirection);
        }
        _characterController.Move(moveDirection * _playerData.MoveSpeed * Time.deltaTime);
    }

    private Vector3 ApplyGravity(Vector3 MoveDirection)
    {
        _playerData.YVelocity += _playerData.Gravity * Time.deltaTime;
        MoveDirection.y = _playerData.YVelocity;
        return MoveDirection;
    }
}

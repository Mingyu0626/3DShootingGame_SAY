using UnityEngine;

public class PlayerBombAttack : IAttackStrategy
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;

    private float _bombHoldStartTime;
    private float _bombThrowingPower;
    private bool _isHoldingBomb;

    public PlayerBombAttack(PlayerAttackController playerAttack, PlayerData playerData)
    {
        _playerAttackController = playerAttack;
        _playerData = playerData;
    }
    public void Enter()
    {
        _playerAttackController.UiWeapon.RefreshUIOnZoomOut();
        Camera.main.fieldOfView = _playerAttackController.ZoomOutSize;
    }
    public void Update()
    {
        if (GameManager.Instance.IsInputBlocked)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1) && 0 < _playerData.CurrentBombCount)
        {
            _bombHoldStartTime = Time.time;
            _isHoldingBomb = true;
        }

        if (Input.GetMouseButtonUp(1) && _isHoldingBomb)
        {
            float heldTime = Time.time - _bombHoldStartTime;
            float normalizedHoldTime = Mathf.Clamp01(heldTime / _playerData.MaxHoldTime);
            _bombThrowingPower = Mathf.Lerp(_playerData.MinBombPower, _playerData.MaxBombPower, normalizedHoldTime);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
            AttackAnimation();
        }
    }
    public void Attack()
    {
        FireBomb();
    }

    public void AttackAnimation()
    {
        Debug.Log("ThrowBomb Animation");
        _playerAttackController.Animator.SetTrigger("ThrowBomb");
    }

    private void FireBomb()
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _playerData.ShootPosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        Vector3 force = Camera.main.transform.forward * _bombThrowingPower;
        if (_playerAttackController.CameraController.CurrentCameraMode == ECameraMode.Quarter)
        {
            force = _playerData.ShootPosition.transform.right * _bombThrowingPower;
        }
        bombRigidbody.AddForce(force, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
    }
}

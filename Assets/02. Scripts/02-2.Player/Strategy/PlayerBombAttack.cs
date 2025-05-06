using UnityEngine;

public class PlayerBombAttack : IAttackStrategy
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;

    private float _bombHoldStartTime;
    private float _fireBombPower;
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
    public void Attack()
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
            _fireBombPower = Mathf.Lerp(_playerData.MinBombPower, _playerData.MaxBombPower, normalizedHoldTime);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
            ShootAnimation();
        }
    }

    public void ShootAnimation()
    {
        Debug.Log("ThrowBomb Animation");
        _playerAttackController.Animator.SetTrigger("ThrowBomb");
    }

    public void FireBomb()
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _playerData.ShootPosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        bombRigidbody.AddForce(_playerAttackController.MainCamera.transform.forward * _fireBombPower, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
    }
}

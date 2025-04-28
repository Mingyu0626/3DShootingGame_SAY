using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunAttackState : IPlayerAttackState
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;
    [Header("Bullet")]
    private float _lastBulletFireTime = -Mathf.Infinity;
    private bool _isContinuousFiring = false;
    private bool _isContinousFireCoolDown = false;
    private Camera _mainCamera;
    public PlayerGunAttackState(PlayerAttackController playerAttackController)
    {
        _playerAttackController = playerAttackController;
        _mainCamera = Camera.main;
        _playerData = _playerAttackController.PlayerData;
    }

    public void Enter()
    {
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerAttackController.PlayerAttackStateContext.
                ChangeState(_playerAttackController.KnifeAttackState);
            Debug.Log("칼 공격 모드로 변경");
        }
        HandleFireBulletInput();
    }

    public void Exit()
    {
    }
    private void HandleFireBulletInput()
    {
        if (GameManager.Instance.IsInputBlocked)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && 0 < _playerData.CurrentBulletCount)
        {
            FireBullet();
        }
        if (Input.GetMouseButton(0) && !_isContinousFireCoolDown
            && _lastBulletFireTime + _playerData.BulletFireInterval <= Time.time
            && 0 < _playerData.CurrentBulletCount)
        {
            _isContinuousFiring = true;
            FireBullet();
        }
        if (Input.GetMouseButtonUp(0) && !_isContinousFireCoolDown)
        {
            if (_isContinuousFiring)
            {
                _isContinuousFiring = false;
                _playerAttackController.StartCoroutineInPlayerAttackState(CooldownCoroutine());
            }
            _playerData.IsBulletFiring = false;
        }
    }
    private void FireBullet()
    {
        _lastBulletFireTime = Time.time;
        _playerData.IsBulletFiring = true;
        _playerData.CurrentBulletCount -= 1;

        Ray ray = new Ray(_playerData.FirePosition.transform.position, _mainCamera.transform.forward);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo))
        {
            CreateHitEffect(hitInfo);
            CreateTracer(_playerData.FirePosition.transform.position, hitInfo.point);

            if (hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = 10,
                    From = _playerAttackController.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
    private void CreateHitEffect(RaycastHit hitInfo)
    {
        _playerData.BulletVFX.transform.position = hitInfo.point;
        _playerData.BulletVFX.transform.forward = hitInfo.normal;
        _playerData.BulletVFX.Play();
    }

    private IEnumerator CooldownCoroutine()
    {
        _isContinousFireCoolDown = true;
        yield return new WaitForSeconds(_playerData.BulletFireCooldown);
        _isContinousFireCoolDown = false;
    }

    private void CreateTracer(Vector3 start, Vector3 end)
    {
        TrailRenderer trail =
            _playerAttackController.InstantiateObject(_playerData.BulletTrailPrefab, start, Quaternion.identity);

        float distance = Vector3.Distance(start, end);
        float duration = distance / _playerData.TracerSpeed;
        _playerAttackController.StartCoroutineInPlayerAttackState(MoveTracer(trail, start, end, duration));
    }

    private IEnumerator MoveTracer(TrailRenderer trail, Vector3 start, Vector3 end, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        _playerAttackController.DestroyObject(trail.gameObject);
    }
}

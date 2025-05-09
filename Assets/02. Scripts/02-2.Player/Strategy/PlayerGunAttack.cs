using Redcode.Pools;
using System.Collections;
using UnityEngine;

public class PlayerGunAttack : IAttackStrategy
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;

    [Header("Bullet")]
    private float _maxFireDistance = 100f;
    private float _lastShootTime = -Mathf.Infinity;
    private bool _isContinuousShooting = false;
    private bool _isContinousShootingCoolDown = false;

    private bool _zoomMode = false;

    public PlayerGunAttack(PlayerAttackController playerAttack, PlayerData playerData)
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

        // 총알 발사(단발 및 연사)
        if (Input.GetMouseButtonDown(0) && 0 < _playerData.CurrentBulletCount
            && _lastShootTime + _playerData.ShootingInterval <= Time.time)
        {
            Attack();
        }
        if (Input.GetMouseButton(0) && 0 < _playerData.CurrentBulletCount
            && _lastShootTime + _playerData.ShootingInterval <= Time.time
            && !_isContinousShootingCoolDown)
        {
            _isContinuousShooting = true;
            Attack();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_isContinuousShooting)
            {
                _isContinuousShooting = false;
                _playerAttackController.StartCoroutineInPlayerAttackState(CooldownCoroutine());
            }
            _playerData.IsShooting = false;
        }

        // 줌인 / 줌아웃
        if (Input.GetMouseButtonDown(1) && 
            _playerAttackController.CameraController.CurrentCameraMode != ECameraMode.Quarter)
        {
            _zoomMode = !_zoomMode;
            if (_zoomMode)
            {
                _playerAttackController.UiWeapon.RefreshUIOnZoomIn();
                Camera.main.fieldOfView = _playerAttackController.ZoomInSize;
            }
            else
            {
                _playerAttackController.UiWeapon.RefreshUIOnZoomOut();
                Camera.main.fieldOfView = _playerAttackController.ZoomOutSize;
            }
        }
    }
    public void Attack()
    {
        Shoot();
    }
    public void AttackAnimation()
    {
        _playerAttackController.Animator.SetTrigger("Shot");
    }
    private void Shoot()
    {
        InstantiateMuzzleVFX();
        AttackAnimation();
        _lastShootTime = Time.time;
        _playerData.IsShooting = true;
        _playerData.CurrentBulletCount -= 1;

        Ray ray = new Ray(_playerData.ShootPosition.transform.position, Camera.main.transform.forward);
        if (_playerAttackController.CameraController.CurrentCameraMode == ECameraMode.Quarter)
        {
            ray.direction = _playerData.ShootPosition.transform.right;
        }

        RaycastHit hitInfo = new RaycastHit();
        int layerMask = LayerMask.GetMask("Enemy", "Obstacle", "Default");
        if (Physics.Raycast(ray, out hitInfo, _maxFireDistance, layerMask))
        {
            InstantiateHitVFX(hitInfo);
            CreateTracer(_playerData.ShootPosition.transform.position, hitInfo.point);
            if (hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = _playerData.GunDamage,
                    From = _playerAttackController.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * _maxFireDistance;
            CreateTracer(_playerData.ShootPosition.transform.position, endPoint);
        }
    }
    private IEnumerator CooldownCoroutine()
    {
        _isContinousShootingCoolDown = true;
        yield return new WaitForSeconds(_playerData.ContinuousShootingCooldown);
        _isContinousShootingCoolDown = false;
    }
    private void InstantiateMuzzleVFX()
    {
        _playerAttackController.InstantiateObject
            (_playerData.MuzzleEffect, _playerData.ShootPosition.transform.position, Quaternion.identity);
    }
    private void InstantiateHitVFX(RaycastHit hitInfo)
    {
        _playerData.BulletVFX.transform.position = hitInfo.point;
        _playerData.BulletVFX.transform.forward = hitInfo.normal;
        _playerData.BulletVFX.Play();
    }
    private void CreateTracer(Vector3 start, Vector3 end)
    {
        TrailRenderer trail = 
            PoolManager.Instance.GetFromPool<TrailRenderer>("BulletTrail");
        trail.transform.position = start;
        trail.transform.rotation = Quaternion.identity;

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
        PoolManager.Instance.TakeToPool<TrailRenderer>("BulletTrail", trail);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Project")]
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private ParticleSystem _bulletVFX;
    [SerializeField]
    private TrailRenderer _bulletTrailPrefab;
    [SerializeField]
    private float _tracerSpeed = 200f;

    [Header("Inspector")]
    [SerializeField]
    private GameObject _firePosition;

    [Header("Bomb")]
    private float _bombHoldStartTime;
    private bool _isHoldingBomb;

    [Header("Bullet")]
    private float _lastBulletFireTime = -Mathf.Infinity;
    private bool _isContinuousFiring = false;
    private bool _isContinousFireCoolDown = false;
    private Camera _mainCamera;

    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleFireBombInput();
        HandleFireBulletInput();
    }

    private void HandleFireBombInput()
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
            float power = Mathf.Lerp(_playerData.MinBombPower, _playerData.MaxBombPower, normalizedHoldTime);
            FireBomb(power);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
        }
    }

    private void FireBomb(float fireBombPower)
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _firePosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        bombRigidbody.AddForce(_mainCamera.transform.forward * fireBombPower, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
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
                StartCoroutine(CooldownCoroutine());
            }
            _playerData.IsBulletFiring = false;
        }
    }

    private void FireBullet()
    {
        _lastBulletFireTime = Time.time;
        _playerData.IsBulletFiring = true;
        _playerData.CurrentBulletCount -= 1;

        Ray ray = new Ray(_firePosition.transform.position, _mainCamera.transform.forward);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo))
        {
            CreateHitEffect(hitInfo);
            CreateTracer(_firePosition.transform.position, hitInfo.point);

            if (hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = 10,
                    From = gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }

    private void CreateHitEffect(RaycastHit hitInfo)
    {
        _bulletVFX.transform.position = hitInfo.point;
        _bulletVFX.transform.forward = hitInfo.normal;
        _bulletVFX.Play();
    }

    private IEnumerator CooldownCoroutine()
    {
        _isContinousFireCoolDown = true;
        yield return new WaitForSeconds(_playerData.BulletFireCooldown);
        _isContinousFireCoolDown = false;
    }

    private void CreateTracer(Vector3 start, Vector3 end)
    {
        TrailRenderer trail = Instantiate(_bulletTrailPrefab, start, Quaternion.identity)
            .GetComponent<TrailRenderer>();

        float distance = Vector3.Distance(start, end);
        float duration = distance / _tracerSpeed;
        StartCoroutine(MoveTracer(trail, start, end, duration));
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
        Destroy(trail.gameObject);
    }
}


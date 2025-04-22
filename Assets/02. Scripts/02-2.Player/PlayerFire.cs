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

    [Header("Inspector")]
    [SerializeField]
    private GameObject _firePosition;

    [Header("Bomb")]
    private float _bombHoldStartTime;
    private bool _isHoldingBomb;
    private float _minBombPower = 10f;
    private float _maxBombPower = 40f;
    private float _maxHoldTime = 2f;

    [Header("Bullet")]
    private float _bulletFireInterval = 0.2f;
    private float _bulletFireCooldown = 5f; 
    private float _lastBulletFireTime = -Mathf.Infinity;
    private bool _isContinuousFiring = false;
    private bool _isContinousFireCoolDown = false;

    private PlayerData _playerData;
    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    private void Update()
    {
        HandleFireBombInput();
        HandleFireBulletInput();
    }
    private void HandleFireBombInput()
    {
        if (Input.GetMouseButtonDown(1) && 0 < _playerData.CurrentBombCount)
        {
            _bombHoldStartTime = Time.time;
            _isHoldingBomb = true;
        }

        if (Input.GetMouseButtonUp(1) && _isHoldingBomb)
        {
            float heldTime = Time.time - _bombHoldStartTime;
            float normalizedHoldTime = Mathf.Clamp01(heldTime / _maxHoldTime);
            float power = Mathf.Lerp(_minBombPower, _maxBombPower, normalizedHoldTime);
            FireBomb(power);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
        }
    }
    private void FireBomb(float fireBombPower)
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _firePosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        bombRigidbody.AddForce(Camera.main.transform.forward * fireBombPower, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
    }
    private void HandleFireBulletInput()
    {
        if (Input.GetMouseButtonDown(0) && 0 < _playerData.CurrentBulletCount)
        {
            FireBullet();
        }
        if (Input.GetMouseButton(0) && !_isContinousFireCoolDown
            && _lastBulletFireTime + _bulletFireInterval <= Time.time
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
        // 2. 레이케스트(레이저)를 생성하고 발사 위치와 진행 방향을 설정하기
        Ray ray = new Ray(_firePosition.transform.position, Camera.main.transform.forward);

        // 3. 레이케스트와 부딪힌 물체의 정보를 저장할 변수를 생성하기
        RaycastHit hitInfo = new RaycastHit();

        // 4. 레이 발사 후, 부딪힌 데이터가 있다면 피격 이펙트를 생성하기
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 5. 피격 이펙트 생성하기
            _bulletVFX.transform.position = hitInfo.point;
            _bulletVFX.transform.forward = hitInfo.normal; // normal은 법선 벡터를 의미한다.
            _bulletVFX.Play();

            // 트레이서 생성
            CreateTracer(_firePosition.transform.position, hitInfo.point);
        }

        // Ray : 레이저(시작위치, 방향)
        // RayCast : 레이저를 발사
        // RayCastHit: 레이저가 물체와 부딪혔다면 부딪힌 물체의 정보를 저장하는 구조체

    }
    private IEnumerator CooldownCoroutine()
    {
        _isContinousFireCoolDown = true;
        yield return new WaitForSeconds(_bulletFireCooldown);
        _isContinousFireCoolDown = false;
    }
    private void CreateTracer(Vector3 start, Vector3 end)
    {
        TrailRenderer trail = Instantiate(_bulletTrailPrefab, start, Quaternion.identity)
            .GetComponent<TrailRenderer>();

        float tracerSpeed = 200f; // 이동 속도
        float distance = Vector3.Distance(start, end);
        float duration = distance / tracerSpeed;
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

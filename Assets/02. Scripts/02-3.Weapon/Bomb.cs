using Redcode.Pools;
using UnityEngine;

public class Bomb : MonoBehaviour, IProduct
{
    [Header("Project")]
    [SerializeField]
    private GameObject _explosionVFXPrefab;

    [SerializeField]
    private ShakeCamera _shakeCamera;
    [SerializeField] 
    private float _explosionRadius = 10f;
    [SerializeField] 
    private int _explosionDamage = 20;
    [SerializeField] 
    private LayerMask _damageableLayer;
    private void Awake()
    {
        _shakeCamera = Camera.main.gameObject.GetComponent<ShakeCamera>();
        _damageableLayer = LayerMask.GetMask("Enemy", "Obstacle", "Default");
    }
    public void Init() {}
    private void OnCollisionEnter(Collision collision)
    {
        // VFX
        Instantiate(_explosionVFXPrefab, transform.position, Quaternion.identity);

        // 데미지 적용
        DealExplosionDamage();

        // 카메라 흔들림
        _shakeCamera.Shake(0.5f, 1f);

        // 오브젝트 풀로 반환
        PoolManager.Instance.TakeToPool<Bomb>(nameof(Bomb), this);
        // BombPool.Instance.ReturnObject(this);
        gameObject.SetActive(false);
    }
    private void DealExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _damageableLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = _explosionDamage,
                    From = gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
}

using Redcode.Pools;
using System.Collections;
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
        PlayExplosionVFX();
        DealExplosionDamage();
        _shakeCamera.Shake(0.5f, 1f);

        PoolManager.Instance.TakeToPool<Bomb>(EPoolObjectName.Bomb.ToString(), this);
    }
    private void PlayExplosionVFX()
    {
        VFX vfx = PoolManager.Instance.GetFromPool<VFX>(EPoolObjectName.VFX_BombExplosion.ToString());
        vfx.OnGetFromPool(transform.position);
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

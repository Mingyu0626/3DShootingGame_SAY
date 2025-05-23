using Redcode.Pools;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : IAttackStrategy
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;

    private float _attackRange = 4f;
    private float _attackAngle = 120f;
    private LayerMask _attackableLayer;

    public PlayerMeleeAttack(PlayerAttackController playerAttack, PlayerData playerData)
    {
        _playerAttackController = playerAttack;
        _playerData = playerData;
        _attackableLayer = LayerMask.GetMask("Enemy", "Obstacle", "Default");
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
        if (Input.GetMouseButtonDown(0))
        {
            AttackAnimation();
        }
    }
    public void Attack()
    {
        MeleeAttack();
        PlayMeleeVFX();
    }
    public void AttackAnimation()
    {
        Debug.Log("MeleeAttack Animation");
        _playerAttackController.Animator.SetTrigger("MeleeAttack");
    }
    private void MeleeAttack()
    {
        Vector3 center = _playerAttackController.transform.position;
        Vector3 forward = _playerAttackController.transform.right;
        List<Collider> targetsInCircularSectorArea = new List<Collider>();
        Collider[] hitColliders = Physics.OverlapSphere(center, _attackRange, _attackableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 dirToTarget = (hitCollider.transform.position - center).normalized;
            float angle = Vector3.Angle(forward, dirToTarget);

            if (angle <= _attackAngle * 0.5f)
            {
                targetsInCircularSectorArea.Add(hitCollider);
            }
        }

        foreach (Collider target in targetsInCircularSectorArea)
        {
            if (target.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Damage damage = new Damage()
                {
                    Value = _playerData.MeleeDamage,
                    From = _playerAttackController.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
    private void PlayMeleeVFX()
    {
        Vector3 basePos = _playerData.ShootPosition.transform.position;
        Vector3 heightOffset = new Vector3(0f, 0.5f, 0f);
        Vector3 vfxPosition = basePos - heightOffset;
        Quaternion rotation = Quaternion.LookRotation(_playerAttackController.transform.forward);
        rotation *= Quaternion.Euler(0f, 45f, 0f);
        VFX vfx = PoolManager.Instance.GetFromPool<VFX>(EPoolObjectName.VFX_Melee.ToString());
        vfx.OnGetFromPool(vfxPosition, rotation);
    }
}

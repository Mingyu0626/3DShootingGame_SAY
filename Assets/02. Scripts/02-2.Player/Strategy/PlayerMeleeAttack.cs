using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : IAttack
{
    private PlayerAttack _playerAttack;
    private PlayerData _playerData;

    private float _attackRange = 4f;
    private float _attackAngle = 120f;
    private LayerMask _enemyLayer;

    public PlayerMeleeAttack(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
        _playerData = playerAttack.PlayerData;
        _enemyLayer = LayerMask.GetMask("Enemy");
    }

    public void Attack()
    {
        if (GameManager.Instance.IsInputBlocked)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        Vector3 center = _playerAttack.transform.position;
        Vector3 forward = _playerAttack.transform.right;
        List<Collider> targetsInCircularSectorArea = new List<Collider>();
        Collider[] hitColliders = Physics.OverlapSphere(center, _attackRange, _enemyLayer);
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
                    Value = 10,
                    From = _playerAttack.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
}

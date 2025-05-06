using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : IAttackStrategy
{
    private PlayerAttackController _playerAttackController;
    private PlayerData _playerData;

    private float _attackRange = 4f;
    private float _attackAngle = 120f;
    private LayerMask _enemyLayer;

    public PlayerMeleeAttack(PlayerAttackController playerAttack, PlayerData playerData)
    {
        _playerAttackController = playerAttack;
        _playerData = playerData;
        _enemyLayer = LayerMask.GetMask("Enemy");
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
        if (Input.GetMouseButtonDown(0))
        {
            ShootAnimation();
        }
    }

    public void MeleeAttack()
    {
        Vector3 center = _playerAttackController.transform.position;
        Vector3 forward = _playerAttackController.transform.right;
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
                    From = _playerAttackController.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }
    }
    public void AttackVFX()
    {
        Vector3 basePos = _playerData.ShootPosition.transform.position;
        Vector3 heightOffset = new Vector3(0f, 0.5f, 0f);
        Vector3 vfxPosition = basePos - heightOffset;

        Quaternion rotation = Quaternion.LookRotation(_playerAttackController.transform.forward);
        rotation *= Quaternion.Euler(0f, 45f, 0f);
        _playerAttackController.InstantiateObject(
            _playerData.BladeEffect,
            vfxPosition,
            rotation);
    }
    public void ShootAnimation()
    {
        Debug.Log("MeleeAttack Animation");
        _playerAttackController.Animator.SetTrigger("MeleeAttack");
    }
}

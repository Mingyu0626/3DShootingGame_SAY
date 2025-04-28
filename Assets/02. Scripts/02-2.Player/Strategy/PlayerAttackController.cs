using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerData _playerData;
    private Camera _mainCamera;

    private float _bombHoldStartTime;
    private bool _isHoldingBomb;

    private PlayerGunAttack _gunAttack;
    public PlayerGunAttack GunAttack { get => _gunAttack; }

    private PlayerMeleeAttack _meleeAttack;
    public PlayerMeleeAttack MeleeAttack { get => _meleeAttack; }

    private PlayerAttackContext _playerAttackContext;

    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    private float _fireBombPower;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _mainCamera = Camera.main;

        _gunAttack = new PlayerGunAttack(this, _playerData);
        _meleeAttack = new PlayerMeleeAttack(this, _playerData);
        _playerAttackContext = GetComponent<PlayerAttackContext>();
        _playerAttackContext.ChangeAttackStrategy(_gunAttack);

        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleChangeAttackInput();
        HandleFireBombInput();
    }
    private void HandleChangeAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerAttackContext.ChangeAttackStrategy(_gunAttack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerAttackContext.ChangeAttackStrategy(_meleeAttack);
        }
    }
    public void StartCoroutineInPlayerAttackState(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    public void StopCoroutineInPlayerAttackState(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
    public T InstantiateObject<T>(T go, Vector3 Start, Quaternion rotation)
        where T : UnityEngine.Object
    {
        return Instantiate(go, Start, rotation).GetComponent<T>();
    }
    public void DestroyObject(GameObject go)
    {
        UnityEngine.Object.Destroy(go);
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
             _fireBombPower = Mathf.Lerp(_playerData.MinBombPower, _playerData.MaxBombPower, normalizedHoldTime);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
            ThrowBombAnimation();
        }
    }

    public void FireBomb()
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _playerData.FirePosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        bombRigidbody.AddForce(_mainCamera.transform.forward * _fireBombPower, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
    }
    private void ThrowBombAnimation()
    {
        Debug.Log("ThrowBomb Animation");
        _animator.SetTrigger("ThrowBomb");
    }
}


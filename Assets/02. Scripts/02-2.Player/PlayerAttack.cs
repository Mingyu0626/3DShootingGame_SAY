using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    private Camera _mainCamera;

    private float _bombHoldStartTime;
    private bool _isHoldingBomb;

    private IAttack _gunAttack, _meleeAttack;
    private PlayerAttackContext _playerAttackContext;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _mainCamera = Camera.main;

        _gunAttack = new PlayerGunAttack(this);
        _meleeAttack = new PlayerMeleeAttack(this);
        _playerAttackContext = GetComponent<PlayerAttackContext>();
        _playerAttackContext.ChangeAttackStrategy(_gunAttack);
    }

    private void Update()
    {
        ChangeAttackStrategy();
        HandleFireBombInput();
    }
    private void ChangeAttackStrategy()
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
            float power = Mathf.Lerp(_playerData.MinBombPower, _playerData.MaxBombPower, normalizedHoldTime);
            FireBomb(power);
            _playerData.CurrentBombCount -= 1;
            _isHoldingBomb = false;
        }
    }

    private void FireBomb(float fireBombPower)
    {
        Bomb bomb = BombPool.Instance.GetObject(BombType.NormalBomb, _playerData.FirePosition.transform.position);
        Rigidbody bombRigidbody = bomb.gameObject.GetComponent<Rigidbody>();
        bombRigidbody.AddForce(_mainCamera.transform.forward * fireBombPower, ForceMode.Impulse);
        bombRigidbody.AddTorque(Vector3.one);
    }
}


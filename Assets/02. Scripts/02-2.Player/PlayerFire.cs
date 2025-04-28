using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private PlayerData _playerData;
    private Camera _mainCamera;

    private float _bombHoldStartTime;
    private bool _isHoldingBomb;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleFireBombInput();
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


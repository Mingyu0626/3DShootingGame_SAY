using UnityEngine;
using System.Collections;

public class PlayerReload : MonoBehaviour
{
    private PlayerData _playerData;
    private PlayerPresenter _playerPresenter;


    [Header("Reload")]
    private bool _isReloading = false;
    private float _reloadDuration = 2f;
    private Coroutine _reloadCoroutine;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerPresenter = GetComponent<PlayerPresenter>();
    }

    private void Update()
    {
        // R 키를 눌렀을 때 재장전 시도
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading && _playerData.CurrentBulletCount < _playerData.MaxBulletCount)
        {
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        float elapsed = 0f;
        while (elapsed < _reloadDuration)
        {
            if (_playerData.IsBulletFiring)
            {
                CancelReload();
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        _playerPresenter.OnBulletCountChanged(_playerData.MaxBulletCount);
        _isReloading = false;
    }

    private void CancelReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _isReloading = false;
    }
}

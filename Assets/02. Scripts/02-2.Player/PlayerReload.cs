using UnityEngine;
using System.Collections;

public class PlayerReload : MonoBehaviour
{
    private PlayerData _playerData;
    [SerializeField]
    private UI_Reload _uiReload;
    [Header("Reload")]
    private bool _isReloading = false;
    private Coroutine _reloadCoroutine;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    private void Update()
    {
        // R 키를 눌렀을 때 재장전 시도
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading && _playerData.CurrentBulletCount < _playerData.MaxBulletCount
            && !_playerData.IsBulletFiring)
        {
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _uiReload.ShowReloading();
        _isReloading = true;
        float elapsed = 0f;
        while (elapsed < _playerData.ReloadDuration)
        {
            if (_playerData.IsBulletFiring)
            {
                CancelReload();
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        _playerData.CurrentBulletCount = _playerData.MaxBulletCount;
        _uiReload.ShowReloadSuccess();
        _isReloading = false;
    }

    private void CancelReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
            _uiReload.ShowReloadCancel();
        }
        _isReloading = false;
    }
}

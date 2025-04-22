using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerData _playerData;
    private UI_PlayerView _playerView;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerView = GetComponent<UI_PlayerView>();
    }

    private void Start()
    {
        _playerData.PlayerStaminaChanged += _playerView.SetSliderPlayerStamina;
        _playerView.InitSliderPlayerStamina(_playerData.StaminaMax);

        _playerData.PlayerBombCountChanged += _playerView.SetTextPlayerBomb;
        _playerView.InitTextPlayerBomb(_playerData.CurrentBombCount, _playerData.MaxBombCount);

        _playerData.PlayerBulletCountChanged += _playerView.SetTextPlayerBullet;
        _playerView.InitTextPlayerBullet(_playerData.CurrentBulletCount, _playerData.MaxBulletCount);
    }

    public void OnStaminaChanged(float stamina)
    {
        _playerData.Stamina += stamina;
        _playerData.PlayerStaminaChanged?.Invoke(_playerData.Stamina);
    }
    public void OnBombCountChanged(int bombCount)
    {
        _playerData.CurrentBombCount = bombCount;
        _playerData.PlayerBombCountChanged?.Invoke
            (_playerData.CurrentBombCount, _playerData.MaxBombCount);
    }
    public void OnBulletCountChanged(int bulletCount)
    {
        _playerData.CurrentBulletCount = bulletCount;
        _playerData.PlayerBulletCountChanged?.Invoke
            (_playerData.CurrentBulletCount, _playerData.MaxBulletCount);
    }
}

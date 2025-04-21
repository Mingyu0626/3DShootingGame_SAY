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
    }

    public void OnStaminaChanged(float stamina)
    {
        _playerData.Stamina -= stamina;
    }
    public float GetStamina()
    {
        return _playerData.Stamina;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerView : MonoBehaviour
{
    [SerializeField]
    private Slider _playerStaminaBar;
    public void InitSliderPlayerStamina(float staminaMax)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.maxValue = staminaMax;
            _playerStaminaBar.value = staminaMax;
        }
    }
    public void SetSliderPlayerStamina(float stamina)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.value = stamina;
        }
    }
}

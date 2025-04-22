using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerView : MonoBehaviour
{
    [SerializeField]
    private Slider _playerStaminaBar;
    [SerializeField]
    private TextMeshProUGUI _playerBombCountTMP;
    [SerializeField]
    private TextMeshProUGUI _playerBulletCountTMP;
    public void InitSliderPlayerStamina(float staminaMax)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.maxValue = staminaMax;
            _playerStaminaBar.value = staminaMax;
        }
    }
    public void InitTextPlayerBomb(int bombCount, int bombCountMax)
    {
        if (!ReferenceEquals(_playerBombCountTMP, null))
        {
            _playerBombCountTMP.text
                = $"Bomb : {bombCount.ToString()} / {bombCountMax.ToString()}";
        }
    }
    public void InitTextPlayerBullet(int bulletCount, int bulletCountMax)
    {
        if (!ReferenceEquals(_playerBulletCountTMP, null))
        {
            _playerBulletCountTMP.text
                = $"Bullet : {bulletCount.ToString()} / {bulletCountMax.ToString()}";
        }
    }
    public void SetSliderPlayerStamina(float stamina)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.value = stamina;
        }
    }
    public void SetTextPlayerBomb(int bombCount, int bombCountMax)
    {
        if (!ReferenceEquals(_playerBombCountTMP, null))
        {
            _playerBombCountTMP.text
                = $"Bomb : {bombCount.ToString()} / {bombCountMax.ToString()}";
        }
    }
    public void SetTextPlayerBullet(int bulletCount, int bulletCountMax)
    {
        if (!ReferenceEquals(_playerBulletCountTMP, null))
        {
            _playerBulletCountTMP.text
                = $"Bullet : {bulletCount.ToString()} / {bulletCountMax.ToString()}";
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerView : MonoBehaviour
{
    [SerializeField]
    private Slider _playerStaminaBar;
    [SerializeField]
    private Slider _playerHealthPointBar;
    [SerializeField]
    private TextMeshProUGUI _playerBombCountTMP;
    [SerializeField]
    private TextMeshProUGUI _playerBulletCountTMP;
    public void InitSliderPlayerStamina(float maxStamina)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.maxValue = maxStamina;
            _playerStaminaBar.value = maxStamina;
        }
    }
    public void InitSliderPlayerHealthPoint(float maxHealthPoint)
    {
        if (!ReferenceEquals(_playerHealthPointBar, null))
        {
            _playerHealthPointBar.maxValue = maxHealthPoint;
            _playerHealthPointBar.value = maxHealthPoint;
        }
    }
    public void InitTextPlayerBomb(int bombCount, int maxBombCount)
    {
        if (!ReferenceEquals(_playerBombCountTMP, null))
        {
            _playerBombCountTMP.text
                = $"Bomb : {bombCount.ToString()} / {maxBombCount.ToString()}";
        }
    }
    public void InitTextPlayerBullet(int bulletCount, int maxBulletCount)
    {
        if (!ReferenceEquals(_playerBulletCountTMP, null))
        {
            _playerBulletCountTMP.text
                = $"Bullet : {bulletCount.ToString()} / {maxBulletCount.ToString()}";
        }
    }
    public void SetSliderPlayerStamina(float stamina)
    {
        if (!ReferenceEquals(_playerStaminaBar, null))
        {
            _playerStaminaBar.value = stamina;
        }
    }
    public void SetSliderPlayerHealthPoint(float healthPoint)
    {
        if (!ReferenceEquals(_playerHealthPointBar, null))
        {
            _playerHealthPointBar.value = healthPoint;
        }
    }
    public void SetTextPlayerBomb(int bombCount, int maxBombCount)
    {
        if (!ReferenceEquals(_playerBombCountTMP, null))
        {
            _playerBombCountTMP.text
                = $"Bomb : {bombCount.ToString()} / {maxBombCount.ToString()}";
        }
    }
    public void SetTextPlayerBullet(int bulletCount, int maxBulletCount)
    {
        if (!ReferenceEquals(_playerBulletCountTMP, null))
        {
            _playerBulletCountTMP.text
                = $"Bullet : {bulletCount.ToString()} / {maxBulletCount.ToString()}";
        }
    }
}

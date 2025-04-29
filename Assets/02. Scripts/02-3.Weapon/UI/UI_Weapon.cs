using UnityEngine.UI;
using UnityEngine;

public class UI_Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiCrossHair;
    public GameObject UiCrossHair { get => _uiCrossHair; set => _uiCrossHair = value; }

    [SerializeField]
    private GameObject _uiSniperZoom;
    public GameObject UiSniperZoom { get => _uiSniperZoom; set => _uiSniperZoom = value; }

    [SerializeField]
    private Image _imageWeaponMode;

    public void RefreshUIOnZoomIn()
    {
        _uiCrossHair.SetActive(false);
        _uiSniperZoom.SetActive(true);
    }
    public void RefreshUIOnZoomOut()
    {
        _uiCrossHair.SetActive(true);
        _uiSniperZoom.SetActive(false);
    }
    public void RefreshWeaponUI(Sprite imageWeaponModeSprite)
    {
        _imageWeaponMode.sprite = imageWeaponModeSprite;
    }
}

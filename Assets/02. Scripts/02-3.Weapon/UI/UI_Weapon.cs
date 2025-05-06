using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UI_Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiCrossHair;
    public GameObject UiCrossHair { get => _uiCrossHair; set => _uiCrossHair = value; }
    private Image _imageCrossHair;

    [SerializeField]
    private GameObject _uiSniperZoom;
    public GameObject UiSniperZoom { get => _uiSniperZoom; set => _uiSniperZoom = value; }


    [SerializeField]
    private Image _imageAttackMode;
    [SerializeField]
    private List<Sprite> _attackModeImageSprites = new List<Sprite>();
    [SerializeField]
    private List<Sprite> _attackModeCrossHairSprites = new List<Sprite>();

    private void Awake()
    {
        _imageCrossHair = _uiCrossHair.GetComponent<Image>();
    }
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
    public void RefreshWeaponUI(int attackModeIndex)
    {
        _imageAttackMode.sprite = _attackModeImageSprites[attackModeIndex];
    }
    public void RefreshWeaponCrossHair(int attackModeIndex)
    {
        _imageCrossHair.sprite = _attackModeCrossHairSprites[attackModeIndex];
    }
}

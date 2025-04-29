using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerData _playerData;
    private Camera _mainCamera;
    public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }

    private PlayerGunAttack _gunAttack;
    public PlayerGunAttack GunAttack { get => _gunAttack; }

    private PlayerMeleeAttack _meleeAttack;
    public PlayerMeleeAttack MeleeAttack { get => _meleeAttack; }

    private PlayerAttackContext _playerAttackContext;

    private PlayerBombAttack _bombAttack;
    public PlayerBombAttack BombAttack { get => _bombAttack; set => _bombAttack = value; }

    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    [SerializeField]
    private UI_Weapon _uiWeapon;
    public UI_Weapon UiWeapon { get => _uiWeapon; set => _uiWeapon = value; }

    [SerializeField]
    private float _zoomInSize = 15f;
    public float ZoomInSize { get => _zoomInSize; set => _zoomInSize = value; }

    [SerializeField]
    private float _zoomOutSize = 60f;
    public float ZoomOutSize { get => _zoomOutSize; set => _zoomOutSize = value; }

    [SerializeField]
    private List<Sprite> _weaponModeSprites = new List<Sprite>();


    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _mainCamera = Camera.main;

        _gunAttack = new PlayerGunAttack(this, _playerData);
        _meleeAttack = new PlayerMeleeAttack(this, _playerData);
        _bombAttack = new PlayerBombAttack(this, _playerData);
        _playerAttackContext = GetComponent<PlayerAttackContext>();
        _playerAttackContext.ChangeAttackStrategy(_gunAttack);

        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleChangeAttackInput();
    }
    private void HandleChangeAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerAttackContext.ChangeAttackStrategy(_gunAttack);
            _uiWeapon.RefreshWeaponUI(_weaponModeSprites[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerAttackContext.ChangeAttackStrategy(_meleeAttack);
            _uiWeapon.RefreshWeaponUI(_weaponModeSprites[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playerAttackContext.ChangeAttackStrategy(_bombAttack);
            _uiWeapon.RefreshWeaponUI(_weaponModeSprites[2]);
        }
    }
    public void StartCoroutineInPlayerAttackState(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    public void StopCoroutineInPlayerAttackState(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
    public T InstantiateObject<T>(T go, Vector3 Start, Quaternion rotation)
        where T : UnityEngine.Object
    {
        return Instantiate(go, Start, rotation).GetComponent<T>();
    }
    public void DestroyObject(GameObject go)
    {
        UnityEngine.Object.Destroy(go);
    }
}


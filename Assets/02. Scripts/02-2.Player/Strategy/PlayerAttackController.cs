using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerData _playerData;
    private PlayerAttackContext _playerAttackContext;

    private Dictionary<EAttackMode, IAttackStrategy> _attackStrategyDict;
    public Dictionary<EAttackMode, IAttackStrategy> AttackStrategyDict 
    { get => _attackStrategyDict; set => _attackStrategyDict = value; }

    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    [SerializeField]
    private CameraController _cameraController;
    public CameraController CameraController { get => _cameraController; }

    [SerializeField]
    private UI_Weapon _uiWeapon;
    public UI_Weapon UiWeapon { get => _uiWeapon; set => _uiWeapon = value; }

    [SerializeField]
    private float _zoomInSize = 15f;
    public float ZoomInSize { get => _zoomInSize; set => _zoomInSize = value; }

    [SerializeField]
    private float _zoomOutSize = 60f;
    public float ZoomOutSize { get => _zoomOutSize; set => _zoomOutSize = value; }

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerAttackContext = GetComponent<PlayerAttackContext>();
        _attackStrategyDict = new Dictionary<EAttackMode, IAttackStrategy>
        {
            { EAttackMode.Gun, new PlayerGunAttack(this, _playerData) },
            { EAttackMode.Melee, new PlayerMeleeAttack(this, _playerData) },
            { EAttackMode.Bomb, new PlayerBombAttack(this, _playerData) }
        };
        _playerAttackContext.ChangeAttackStrategy(_attackStrategyDict[EAttackMode.Gun]);

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
            _playerAttackContext.ChangeAttackStrategy(_attackStrategyDict[EAttackMode.Gun]);
            _uiWeapon.RefreshWeaponUI((int)EAttackMode.Gun);
            _uiWeapon.RefreshWeaponCrossHair((int)EAttackMode.Gun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerAttackContext.ChangeAttackStrategy(_attackStrategyDict[EAttackMode.Melee]);
            _uiWeapon.RefreshWeaponUI((int)EAttackMode.Melee);
            _uiWeapon.RefreshWeaponCrossHair((int)EAttackMode.Melee);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playerAttackContext.ChangeAttackStrategy(_attackStrategyDict[EAttackMode.Bomb]);
            _uiWeapon.RefreshWeaponUI((int)EAttackMode.Bomb);
            _uiWeapon.RefreshWeaponCrossHair((int)EAttackMode.Bomb);
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


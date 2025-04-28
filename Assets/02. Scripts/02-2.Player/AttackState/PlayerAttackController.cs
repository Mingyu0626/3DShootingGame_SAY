using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("State System")]
    private IPlayerAttackState _knifeAttackState, _gunAttackState;
    public IPlayerAttackState KnifeAttackState => _knifeAttackState;
    public IPlayerAttackState GunAttackState => _gunAttackState;

    private PlayerAttackStateContext _playerAttackStateContext;
    public PlayerAttackStateContext PlayerAttackStateContext => _playerAttackStateContext;

    [Header("Components")]
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _playerAttackStateContext = new PlayerAttackStateContext(this);
        _knifeAttackState = new PlayerMeleeAttackState(this);
        _gunAttackState = new PlayerGunAttackState(this);
    }
    private void Start()
    {
        _playerAttackStateContext.ChangeState(_gunAttackState);
    }

    private void Update()
    {
        _playerAttackStateContext.CurrentState.Update();
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

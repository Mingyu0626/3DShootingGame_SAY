using DG.Tweening;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public interface IItem
{

}

public abstract class BaseItem : MonoBehaviour
{
    private PlayerData _playerData;
    protected PlayerData PlayerData { get => _playerData; }

    private Collider _collider;
    private Rigidbody _rigidbody;

    [Header("Amount")]
    [SerializeField]
    private float _amount;
    public float Amount { get => _amount; private set => _amount = value; }


    [Header("Get & Disapper Logic")]
    [SerializeField]
    private float _collisionTimeToApply = 1f;
    public float CollisionTimeToApply { get => _collisionTimeToApply; }

    [SerializeField] 
    private float _disappearTime = 10f;


    [Header("VFX")]
    [SerializeField] private GameObject _getItemVFXPrefab;

    [Header("Bazier")]
    [SerializeField]
    private float _moveSpeed = 3f;
    private Vector3 _controlVector = Vector3.zero;


    [Header("Item Rotation")]
    [SerializeField] 
    private float _rotateSpeed = 180f;

    private void Awake()
    {
        _playerData = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerData>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(RotateCoroutine());
        Destroy(gameObject, _disappearTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GetItemTimer());
        }
    }
    public abstract void ApplyItem();
    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            transform.Rotate(0f, _rotateSpeed * Time.deltaTime, 0f, Space.World);
            yield return null;
        }
    }
    public void PlayVFX()
    {
        // VFXType vfxType = _getItemVFXPrefab.GetComponent<VFX>().VfxType;
        // VFXPool.Instance.GetObject(vfxType, Player.Instance.transform.position);
    }

    private IEnumerator GetItemTimer()
    {
        yield return new WaitForSeconds(_collisionTimeToApply);
        // PlayVFX();
        ApplyItem();
    }
    public void GoToPlayer()
    {
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        StartCoroutine(GoToPlayerCoroutine());
    }
    private IEnumerator GoToPlayerCoroutine()
    {
        Tween moveTween;
        while (true)
        {
            moveTween = transform.DOMove(PlayerManager.Instance.transform.position, 1f / _moveSpeed)
            .SetEase(Ease.Linear); // 이 경우는 바운스 같은 건 별로 안 어울림
            yield return moveTween.WaitForCompletion();
        }
    }
    private Vector3 Bazier(Vector3 start, Vector3 center, Vector3 end, float time)
    {
        // 새로운 위치1 p1 = 시작점과 제어점 사이의 보간
        // 새로운 위치2 p2 = 시작점과 제어점 사이의 보간
        // 최종 위치 = 새로운 위치1 + 새로운 위치2
        Vector3 p1 = Vector2.Lerp(start, center, time);
        Vector3 p2 = Vector3.Lerp(center, end, time);
        Vector3 final = Vector3.Lerp(p1, p2, time);
        return final;
    }
}

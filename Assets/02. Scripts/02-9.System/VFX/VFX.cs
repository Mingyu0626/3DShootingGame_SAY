using System.Collections;
using Redcode.Pools;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFX : MonoBehaviour
{
    [SerializeField]
    private EPoolObjectName _poolObjectName;

    private ParticleSystem[] _particleSystems;

    private void Awake()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>(true); // 자식 포함 전체 가져오기
    }

    public void OnGetFromPool(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        StartCoroutine(VFXCoroutine());
    }

    public void OnGetFromPool(Vector3 position, Quaternion quaternion)
    {
        transform.position = position;
        transform.rotation = quaternion;
        StartCoroutine(VFXCoroutine());
    }

    private IEnumerator VFXCoroutine()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }

        // 모든 파티클이 다 죽을 때까지 기다림
        bool isAlive;
        do
        {
            isAlive = false;
            foreach (ParticleSystem ps in _particleSystems)
            {
                if (ps.IsAlive(true)) // 자식 포함 체크
                {
                    isAlive = true;
                    break;
                }
            }
            yield return null;
        } while (isAlive);

        PoolManager.Instance.TakeToPool<VFX>(_poolObjectName.ToString(), this);
    }
}

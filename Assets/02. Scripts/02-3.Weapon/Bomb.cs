using UnityEngine;

public class Bomb : MonoBehaviour, IProduct
{
    // 목표 : 마우스 우클릭을 하면, 카메라가 바라보는 방향으로 수류탄 던지기
    // 1. 수류탄 오브젝트 만들기
    // 2. 오른쪽 버튼 입력 받기
    // 3. 발사 위치에 수류탄 생성하기
    // 4. 생성된 수류탄을 카메라 방향으로 물리적인 힘 가하기
    [Header("Project")]
    [SerializeField]
    private GameObject _explosionVFXPrefab;

    [SerializeField]
    private ShakeCamera _shakeCamera;
    private void Awake()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosionVFXPrefab, transform.position, Quaternion.identity);
        BombPool.Instance.ReturnObject(this);
        _shakeCamera.Shake(0.5f, 0.5f);
        gameObject.SetActive(false);
    }

    public void Init()
    {
    }
}

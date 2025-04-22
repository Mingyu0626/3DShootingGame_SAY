using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("Project")]
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private ParticleSystem _bulletVFX;

    [Header("Inspector")]
    [SerializeField]
    private GameObject _firePosition;

    private float _fireBombPower = 20f;
    private float _fireBulletPower = 30f;
    private void Awake()
    {
        
    }

    private void Update()
    {
        FireBomb();
        FireBullet();
    }
    private void FireBomb()
    {
        // 0 : 마우스 왼쪽 버튼, 1 : 마우스 오른쪽 버튼, 2 : 휠 클릭
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bombGO = Instantiate(_bombPrefab, _firePosition.transform.position, Quaternion.identity);
            Rigidbody bombRigidbody = bombGO.GetComponent<Rigidbody>();
            bombRigidbody.AddForce(Camera.main.transform.forward * _fireBombPower, ForceMode.Impulse);
            bombRigidbody.AddTorque(Vector3.one);
        }
    }
    private void FireBullet()
    {
        // 총알 발사 : 레이저 방식
        // 목표 : 마우스 좌클릭을 하면, 카메라가 바라보는 방향으로 총알 발사하기

        // 1. 왼쪽 버튼 입력 받기
        if (Input.GetMouseButtonDown(0))
        {
            // 2. 레이케스트(레이저)를 생성하고 발사 위치와 진행 방향을 설정하기
            Ray ray = new Ray(_firePosition.transform.position, Camera.main.transform.forward);

            // 3. 레이케스트와 부딪힌 물체의 정보를 저장할 변수를 생성하기
            RaycastHit hitInfo = new RaycastHit();

            // 4. 레이 발사 후, 부딪힌 데이터가 있다면 피격 이펙트를 생성하기
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 5. 피격 이펙트 생성하기
                _bulletVFX.transform.position = hitInfo.point;
                _bulletVFX.transform.forward = hitInfo.normal; // normal은 법선 벡터를 의미한다.
                _bulletVFX.Play();
                //GameObject bulletGO = Instantiate(_bombPrefab, hitInfo.point, Quaternion.identity);
                //bulletGO.transform.forward = hitInfo.normal;
                //Rigidbody bulletRigidbody = bulletGO.GetComponent<Rigidbody>();
                //bulletRigidbody.AddForce(hitInfo.normal * _fireBulletPower, ForceMode.Impulse);
                //bulletRigidbody.AddTorque(Vector3.one);

                // 게임 수학 : 선형대수학(스칼라, 벡터, 행렬), 기하학(삼각함수)
            }
        }

        // Ray : 레이저(시작위치, 방향)
        // RayCast : 레이저를 발사
        // RayCastHit: 레이저가 물체와 부딪혔다면 부딪힌 물체의 정보를 저장하는 구조체
    }
}

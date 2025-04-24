using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA; // 시작 위치
    
    [SerializeField]
    private Transform _pointB; // 끝 위치

    [SerializeField]
    private float _speed = 1.0f; // 이동 속도

    void Update()
    {
        // 시간에 따라 PingPong 값을 계산 (0~1 사이)
        float time = Mathf.PingPong(Time.time * _speed, 1f);

        // A와 B 사이를 Lerp로 보간
        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, time);
    }
}
using UnityEngine;

public class Bomb : MonoBehaviour, IProduct
{
    [Header("Project")]
    [SerializeField]
    private GameObject _explosionVFXPrefab;

    [SerializeField]
    private ShakeCamera _shakeCamera;
    private void Awake()
    {
        _shakeCamera = Camera.main.gameObject.GetComponent<ShakeCamera>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosionVFXPrefab, transform.position, Quaternion.identity);
        BombPool.Instance.ReturnObject(this);
        _shakeCamera.Shake(0.5f, 1f);
        gameObject.SetActive(false);
    }

    public void Init()
    {
    }
}

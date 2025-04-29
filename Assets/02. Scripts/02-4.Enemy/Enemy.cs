using UnityEngine;

public class Enemy : MonoBehaviour, IProduct
{
    [Header("Prefab")]
    [SerializeField]
    private Canvas _canvasForEnemyHPBar;

    [Header("Item")]
    [SerializeField]
    private GameObject _goldPrefab;

    public void Init()
    {
        if (!ReferenceEquals(_canvasForEnemyHPBar, null) && _canvasForEnemyHPBar.renderMode == RenderMode.WorldSpace)
        {
            _canvasForEnemyHPBar.worldCamera = Camera.main;
        }
    }
    public void SpawnGold()
    {
        Instantiate(_goldPrefab, transform.position, Quaternion.Euler(90f, 0f, 0f));
    }
    public void SpawnRandomItem()
    {

    }
} 
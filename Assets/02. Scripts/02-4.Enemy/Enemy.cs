using UnityEngine;

public class Enemy : MonoBehaviour, IProduct
{
    [Header("Prefab")]
    [SerializeField]
    private Canvas _canvasForEnemyHPBar;

    public void Init()
    {
        if (!ReferenceEquals(_canvasForEnemyHPBar, null) && _canvasForEnemyHPBar.renderMode == RenderMode.WorldSpace)
        {
            _canvasForEnemyHPBar.worldCamera = Camera.main;
        }
    }
} 
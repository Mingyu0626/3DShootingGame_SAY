using UnityEngine;

public enum EEnemyType
{
    Normal,
    AlwaysTrace,
    Elite
}

public class EnemyFactory : Factory<Enemy>
{
    public override Enemy GetProduct(GameObject prefab, Vector3 position)
    {
        GameObject enemyObject = Instantiate(prefab, position, Quaternion.identity);
        return enemyObject.GetComponent<Enemy>();
    }
} 
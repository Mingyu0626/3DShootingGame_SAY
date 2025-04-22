using UnityEngine;
public enum BombType
{
    NormalBomb
}
public class BombFactory : Factory<Bomb>
{
    public override Bomb GetProduct(GameObject bombGO, Vector3 position)
    {
        return Instantiate(bombGO, position, Quaternion.identity)
            .GetComponent<Bomb>();
    }
}

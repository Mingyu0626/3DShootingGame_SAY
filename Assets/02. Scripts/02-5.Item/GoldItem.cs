using UnityEngine;

public class GoldItem : BaseItem
{
    public override void ApplyItem()
    {
        // 골드 증가
        Debug.Log("골드 증가");
        Destroy(gameObject);
    }
}

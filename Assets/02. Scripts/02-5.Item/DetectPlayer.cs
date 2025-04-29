using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private BaseItem _baseItem;
    private void Awake()
    {
        _baseItem = GetComponentInParent<BaseItem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!ReferenceEquals(_baseItem, null) && other.CompareTag("Player"))
        {
            _baseItem.GoToPlayer();
        }
    }

}

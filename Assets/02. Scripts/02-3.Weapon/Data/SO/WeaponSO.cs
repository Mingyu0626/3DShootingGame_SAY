using UnityEngine;

public enum EWeaponType
{
    Gun,
    Knife,
    Bomb,
    


    Count
}


[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Objects/WeaponDataSO")]
public class WeaponSO : ScriptableObject
{
    public EWeaponType WeaponType;
    public Sprite ImageWeaponMode;
    public float Damage;
}

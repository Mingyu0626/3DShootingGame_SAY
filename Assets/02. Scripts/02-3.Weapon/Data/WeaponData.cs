using UnityEngine;

public class WeaponData
{
    public readonly WeaponSO WeaponDataSO;

    private float _currentDamage;
    public float CurrentDamage
    {
        get => _currentDamage;
        set => _currentDamage = value;
    }

    public WeaponData(WeaponSO weaponDataSO)
    {
        WeaponDataSO = weaponDataSO;
    }
}

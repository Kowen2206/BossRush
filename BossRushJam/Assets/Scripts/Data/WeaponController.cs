using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon[] _weapons;
    public static WeaponController  instance; 

    void Awake()
    {
        if(instance == null)
        instance = this;
        else
        Destroy(this);
    }
    
    public void CreateWeapon(WeaponsList type, Vector3 position, float weaponHealt = 100)
    {
        foreach (Weapon weapon in _weapons)
        {
            if(weapon.WeaponTye == type)
            {
              Weapon _usedWeapon =  Instantiate(weapon, position, Quaternion.identity);
              _usedWeapon.Durability = weaponHealt;
            }
        }
    }
}

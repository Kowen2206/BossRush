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
        Weapon weapon =  GetWeapon(type);
        Weapon _usedWeapon =  Instantiate(weapon, position, Quaternion.identity);
              _usedWeapon.Durability = weaponHealt;
    }

    public Weapon GetWeapon(WeaponsList type)
    {
        foreach (Weapon weapon in _weapons)
        {
            if(weapon.WeaponTye == type)
            {
              return weapon;
            }
        }
        return null;
    }

    public Sprite GetWeaponImage(WeaponsList type)
    {
         return  GetWeapon(type).WeaponImage;
    }
}

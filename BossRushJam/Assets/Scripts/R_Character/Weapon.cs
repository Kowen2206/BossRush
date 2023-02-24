using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Weapon : Item
{
    [SerializeField] WeaponsList _weaponType;
    [SerializeField] float _damage;
    float _baseDamage = 5;
    CircleCollider2D _collider;
    Rigidbody2D _rigid;
    [SerializeField] bool _moveOnAttack, _decreaseBySecond;
    [SerializeField] float _durability = 100f, _decrementPercentage;
    [SerializeField] bool _makeConstantDamage;
    
    public bool MakeConstantDamage{ get => _makeConstantDamage;}

    public bool MoveOnAttack{ get => _moveOnAttack;}

    public float WeaponDurability{ get => _durability; set => _durability = value; }

    public WeaponsList WeaponTye {get => _weaponType; set => _weaponType = value;}
    public float Damage {get => _baseDamage; set => _baseDamage = value;}

    public float DecrementPercentage
    {get => _decrementPercentage; set => _decrementPercentage = value;}

    public float Durability
    {get => _durability; set => _durability = value;}

    public bool DecreaseBySecond{get => _decreaseBySecond;}
    void Start()
    {
            _rigid = GetComponent<Rigidbody2D>();
            _rigid.isKinematic = true;
            _collider = GetComponent<CircleCollider2D>();
            _collider.isTrigger = true;
    }
}

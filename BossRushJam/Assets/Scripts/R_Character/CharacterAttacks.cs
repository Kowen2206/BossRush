using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAttacks : MonoBehaviour
{
    MoveCharacter _moveCharacter;
    [SerializeField] WeaponsList _currentWeaponType;
    [SerializeField] WeponDurability _weaponDurability;
    Weapon _lastWeaponInRange;
    [SerializeField] float _damage;
    [SerializeField] GameObject AcidBullet, _fireR, _fireL;
    float _timeSinceLastAttack, _defaultDamage;
    [SerializeField] Transform _leftDistanceOriginAttack, _rightDistanceOriginAttack;
    [SerializeField] UnityEvent OnAttackWithConstantWeapon, OnAttackWithPunchs, OnflamethrowerAttack, OnAcidAttack, OnScalpelAttack, OnChainSawAttack, OnStopAttack, OnAttack;
    
    [SerializeField] MakeDamage _characterDamageColliderLeft, _characterDamageColliderRight;
    public float Damage{ get => _damage; }
    void Awake()
    {
        _weaponDurability = GetComponent<WeponDurability>();
        _moveCharacter = GetComponent<MoveCharacter>();
        _currentWeaponType = WeaponsList.None;
        HideFire();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        Attack();
        if(Input.GetMouseButtonUp(0))
        OnStopAttack?.Invoke();
        if(Input.GetKeyDown(KeyCode.F))
        ChangeWeapon();
        _timeSinceLastAttack += Time.deltaTime;
    }

    void Attack()
    {
        switch (_currentWeaponType)
        {
            case WeaponsList.None:
                OnAttackWithPunchs?.Invoke();
                break;
            case WeaponsList.FlameThrower:
                OnflamethrowerAttack?.Invoke();
                break;
            case WeaponsList.AcidCanon:
                OnAcidAttack?.Invoke();
                break;
            case WeaponsList.Scalpel:
                OnScalpelAttack?.Invoke();
                break;
            case WeaponsList.ChainSaw:
                OnChainSawAttack?.Invoke();
                break;
            default:
                break;
        }

        if(_timeSinceLastAttack >= 1 && _weaponDurability._decreaseBySecond) 
        {
            OnAttackWithConstantWeapon?.Invoke();
            _timeSinceLastAttack = 0;
        }
        OnAttack?.Invoke();
    }

//Los que no decrementan por segundo los llamo en la animación, además fuerxo a que termine de reproducirse la animación.
//Giphy be animated
    public void SpawnAcidBalls()
    {
        if(_timeSinceLastAttack < .5f) return;
        GameObject acidBullet;
        if(_moveCharacter._hDirection == Vector3.right)
        acidBullet =  Instantiate(AcidBullet,_rightDistanceOriginAttack.position, Quaternion.identity);
        else
        acidBullet = Instantiate(AcidBullet,_leftDistanceOriginAttack.position, Quaternion.identity);
        Proyectil proyectil = acidBullet.GetComponent<Proyectil>();
        proyectil.customDirection = true;
        proyectil._direction = _moveCharacter._hDirection;
        proyectil.StartShoot();
        _timeSinceLastAttack = 0;  
    }

    public void SpawnFire()
    {
        if(_moveCharacter._hDirection == Vector3.right)
        {    
            _fireR.SetActive(true);
            _fireL.SetActive(false);
        }
        else
        {
            _fireR.SetActive(false);
            _fireL.SetActive(true);
        }
    }

    public void HideFire()
    {
        _fireL.SetActive(false);
        _fireR.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Weapon")) _lastWeaponInRange = other.GetComponent<Weapon>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Weapon"))
        _lastWeaponInRange = null;
    }
    
    void ChangeWeapon()
    {   if(!_lastWeaponInRange) return;
            if(_lastWeaponInRange.WeaponTye != _currentWeaponType)
            {
                if(_currentWeaponType != WeaponsList.None)
                {
                  WeaponController.instance.CreateWeapon(_currentWeaponType, _lastWeaponInRange.transform.position, _weaponDurability.durability);
                }
                SetCurrentWeapon();
            }
    }
    
    public void SetCurrentWeapon()
    {
        _currentWeaponType = _lastWeaponInRange.WeaponTye;
        _weaponDurability.durability = _lastWeaponInRange.Durability;
        _weaponDurability._decrementPercentage = _lastWeaponInRange.DecrementPercentage;
        _weaponDurability._decreaseBySecond = _lastWeaponInRange.DecreaseBySecond;
        _damage = _lastWeaponInRange.Damage;
        UIGamePlayController.instance.SetSelectedItem(_currentWeaponType);
        SetDamage();
        _lastWeaponInRange.GetComponent<DestroyObject>().DestroyWithDelay(.02f);
        _lastWeaponInRange = null;
        
    }

    public void SetDefaultAttack()
    {
        _currentWeaponType = WeaponsList.None;
        _damage = _defaultDamage;
    }

    void SetDamage()
    {
        _characterDamageColliderLeft.Damage = _characterDamageColliderRight.Damage = _damage;
        _characterDamageColliderLeft.MakeConstantDamageOnStay = _characterDamageColliderRight.MakeConstantDamageOnStay = _lastWeaponInRange.MakeConstantDamage;
        
    }
    
}
public enum WeaponsList
    {
        None,
        AcidCanon,
        FlameThrower,
        Scalpel,
        ChainSaw
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MakeDamage : MonoBehaviour
{
    [SerializeField] string _targetId;
    [SerializeField] List<string> _multipleTargetsId = new List<string>();
    [SerializeField] float _damage;
    [SerializeField] bool _isTrigger = true;
    [SerializeField] bool _isKinematic = true;
    [SerializeField] bool _makeConstantDamage;
    [SerializeField] bool _makingDamage = true;

    public bool MakingDamage
    {
        get => _makingDamage; 
        set{
            Debug.Log("MakingDamage");
            _makingDamage = value;
            }
    }
    public float Damage
    {
        get => _damage; set => _damage = value;
    }

    public bool MakeConstantDamageOnStay{get => _makeConstantDamage; set => _makeConstantDamage = value;}

    void Awake()
    {
        GetComponent<Rigidbody2D>().isKinematic = _isKinematic;
        GetComponent<BoxCollider2D>().isTrigger = _isTrigger;
        _multipleTargetsId.Add(_targetId);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        foreach(string target in _multipleTargetsId)
        {
            
            if(other.CompareTag(target) && _makingDamage)
            {
                Debug.Log("TriggerENTER" + target);
                other.gameObject.GetComponent<HealtController>().decreaseHealt(_damage);
                _makingDamage = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if(_makeConstantDamage)
        foreach(string target in _multipleTargetsId)
        {
           if(other.CompareTag(target) && _makingDamage)
            {
                Debug.Log("TriggerSTAY" + target);
                other.gameObject.GetComponent<HealtController>().decreaseHealt(_damage);
                _makingDamage = false;
            }
        }
    }
}

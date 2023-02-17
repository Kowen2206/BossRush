using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MakeDamage : MonoBehaviour
{
    [SerializeField] string _targetId;
    [SerializeField] float _damage;
    [SerializeField] bool _isTrigger = true;
    [SerializeField] bool _isKinematic = true;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Rigidbody2D>().isKinematic = _isKinematic;
        GetComponent<BoxCollider2D>().isTrigger = _isTrigger;
    }


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_targetId))
        {
            other.gameObject.GetComponent<HealtController>().decreaseHealt(_damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag(_targetId))
        {
            other.gameObject.GetComponent<HealtController>().decreaseHealt(_damage);
        }
    }
}

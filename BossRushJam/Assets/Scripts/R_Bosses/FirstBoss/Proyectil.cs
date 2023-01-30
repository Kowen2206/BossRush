using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _proyectilSpeed = 15, _secondsToAutoDestroy = 25;
    Vector3  _direction;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _direction = _target.position - transform.position;
        _direction.Normalize();
    }

    void Update()
    {
        transform.position += _direction * _proyectilSpeed * Time.deltaTime;
        _secondsToAutoDestroy -= Time.deltaTime;
        if(_secondsToAutoDestroy <= 0) Destroy(gameObject);
    }

}
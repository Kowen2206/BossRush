using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Proyectil : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] string targetTag = "Player";
    [SerializeField] bool _autoDestroy = true, _shootOnStart= true, _stopInTargetPosition, _ignoreCollisionObjects, _drawTrayectoryLine;
    bool _shoot;
    public bool customDirection;
    [SerializeField] float _proyectilSpeed = 15, _proyectilDuration = 25, _damage = 5;
    [SerializeField] UnityEvent OnAutoDestroy, OnImpact, OnImpactWithTarget,OnFinishDuration, OnGetInTargetPosition;
    public Vector3  _direction = Vector3.zero;
    public Vector3 _targetPosition;
    float _remainingDuration = 0;
    [SerializeField] GameObject _trayectoryLinePrefab;
     GameObject _currenTrayectoryLine;

    public float ProyectilDuration { get => _proyectilDuration; set => _proyectilDuration = value; }
    public float ProyectilSpeed { get => _proyectilSpeed; set => _proyectilSpeed = value;  }
    public float Damage { get => _damage; set => _damage = value;  }
    public bool IsInTargetPosition {get; set;}
    public bool DrawTrayectoryLine {get => _drawTrayectoryLine; set => _drawTrayectoryLine = value;}
    
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
        if(_drawTrayectoryLine) CreateLine();
        _target = GameObject.FindGameObjectWithTag(targetTag).transform;
        if(_shootOnStart) StartShoot();
    }
    
    public void StartShoot()
    {
        if(_drawTrayectoryLine) DestroyLine();
        if(!_target) _target = GameObject.FindGameObjectWithTag(targetTag).transform;
        _remainingDuration = _proyectilDuration;
        if(!customDirection) CalculateDirection();
        IsInTargetPosition = false;
        _shoot = true;
    }

    public Transform Target
    {
        get; set;
    }

    public void CalculateDirection()
    {
        _targetPosition = _target.position;
        _direction = _targetPosition - transform.position;
        _direction.Normalize();
    }

    void Update()
    {
        if(_remainingDuration <= 0)
        { 
            if(_autoDestroy)
            {
                OnAutoDestroy?.Invoke();
                DestroyProeyctil();
            }
            else
            OnFinishDuration?.Invoke();
        }
        else
        {
            if(_shoot)
            {
                transform.position += _direction * _proyectilSpeed * Time.deltaTime;
                _remainingDuration -= Time.deltaTime;
                Debug.Log(gameObject.name + "shouldMove");
            }
        }
        if(_stopInTargetPosition && Vector3.Distance(_targetPosition, transform.position) < .5f && _shoot)
        {
            Debug.Log("MiediendoEsta");
           Stop();
           OnGetInTargetPosition?.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            OnImpact?.Invoke();
            HealtController playerHealt = other.gameObject.GetComponent<HealtController>();
            playerHealt.decreaseHealt(_damage);
            Stop();
        }
        else
        if(other.CompareTag("collisionObject") && !_ignoreCollisionObjects)
        {
            OnImpact?.Invoke();
            Stop();
        }
    }

    public void DestroyProeyctil()
    {
        Destroy(gameObject);
    }

    public void Stop()
    {
       
        IsInTargetPosition = true;
        _shoot = false;
    }

    public void CreateLine()
    {
      _currenTrayectoryLine = Instantiate(_trayectoryLinePrefab, transform.position, Quaternion.identity);
      _currenTrayectoryLine.GetComponent<TrayectoryLine>().GrowInYaxis = _direction == Vector3.up;
    }

    public void DestroyLine()
    {
        _currenTrayectoryLine.GetComponent<DestroyObject>().DestroyWithDelay();
    }
}
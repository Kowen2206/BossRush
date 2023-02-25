using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCharacter : MonoBehaviour
{

    //Todo: Hacer que el personaje ataque en la ultima dirección que quedo viendo
    SpriteRenderer _spriteRenderer;
    [SerializeField] float _moveSpeed, _attackSpeed, _dashSpeed = 25, _dashDuration;
    [SerializeField] float _defaultMoveSpeed, _deafultDashDuration, _dashCounter, _dashWaitDuration = 4;
    float _hMove, _vMove, _remainingDashWaitDuration;
    bool isMoving, _dashing;
    Vector3 _lastPosition = Vector3.zero;
    [SerializeField] UnityEvent OnWalk, OnStay, OnDash, OnStopDash;
    public Vector3 _hDirection = Vector3.right, _vDirection = Vector3.up;
    Rigidbody2D _rigid;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _defaultMoveSpeed = _moveSpeed;
        _deafultDashDuration = _dashDuration;
        _dashDuration = 0;
    }

    //Each weapon can define the speed of the character while Attack
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float AttackMoveSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
    }

    public void SetDefaultSpeed()
    {
        _moveSpeed = _defaultMoveSpeed;
        _attackSpeed = _defaultMoveSpeed;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            _hDirection =  Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        { 
            _spriteRenderer.flipX = false; 
            _hDirection =  Vector3.right;
        }
        if(_lastPosition == transform.position)
        {
            isMoving = false;
            OnStay?.Invoke();
        }
        _hMove = Input.GetAxis("Horizontal");
        _vMove = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.A))
            MoveTowards(Vector3.left);
        if(Input.GetKey(KeyCode.D))
            MoveTowards(Vector3.right);
        if(Input.GetKey(KeyCode.W))
            MoveTowards(Vector3.up);
        if(Input.GetKey(KeyCode.S))
            MoveTowards(Vector3.down);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _dashing = true;
            
        }
        UpdateDashVariables();
        SetDash();
        

        _lastPosition = transform.position;
        
    }

    void SetDash()
    {
        if(_dashDuration > 0 && _dashing)
        {
            if(_dashDuration == _deafultDashDuration) {OnDash?.Invoke();}
            Vector3 dashDirection = transform.position - _lastPosition; 
            _moveSpeed = _dashSpeed; 
            MoveTowards(dashDirection);
            _moveSpeed = _defaultMoveSpeed;
            _dashDuration -= Time.deltaTime;
            UIGamePlayController.instance.UpdateDashBar(_dashDuration);
            if(_dashDuration <= 0) {OnStopDash?.Invoke();_remainingDashWaitDuration = _dashWaitDuration;}
        }
    }

    void UpdateDashVariables()
    {
        if(_dashDuration < _deafultDashDuration && _remainingDashWaitDuration >= 0)
        {
            _dashing = false;
            _remainingDashWaitDuration -= Time.deltaTime;
            FillDashUI();
            if(_remainingDashWaitDuration < 0)
            {
                _dashDuration = _deafultDashDuration;
                FillDashUI();
            }
        }
    }

    void FillDashUI()
    {
       float percentage = _remainingDashWaitDuration/_dashWaitDuration;
       UIGamePlayController.instance.UpdateDashBar(percentage);
    }

    void MoveTowards(Vector3 direction)
    {
        if(_moveSpeed != 0)
        {
            isMoving = true;
            OnWalk?.Invoke();
            transform.position += direction * Time.deltaTime * _moveSpeed;
        }
    }

    
}

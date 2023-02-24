using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCharacter : MonoBehaviour
{

    //Todo: Hacer que el personaje ataque en la ultima dirección que quedo viendo
    SpriteRenderer _spriteRenderer;
    [SerializeField] float _moveSpeed, _attackSpeed, _dashSpeed = 25, _dashDuration;
    [SerializeField] float _defaultMoveSpeed, _dashCounter, _dashWaitDuration = 4;
    float _hMove, _vMove;
    bool isMoving, _dashing;
    Vector3 _lastPosition = Vector3.zero;
    [SerializeField] UnityEvent OnWalk, OnStay;
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
        
        SetDash();
        UpdateDashVariables();

        _lastPosition = transform.position;
        
    }

    void SetDash()
    {
        if(_dashCounter < _dashDuration && _dashing)
        {
            Vector3 dashDirection = transform.position - _lastPosition; 
            _moveSpeed = _dashSpeed;   
            MoveTowards(dashDirection);
            _moveSpeed = _defaultMoveSpeed;
            _dashCounter += Time.deltaTime;
        }
        
        
    }

    void UpdateDashVariables()
    {
        if(_dashCounter >= _dashDuration)
            {
                _dashing = false;
                _dashWaitDuration -= Time.deltaTime;
                if(_dashWaitDuration < 0)
                {
                    _dashCounter = 0;
                    _dashWaitDuration = 4;
                }
            }
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

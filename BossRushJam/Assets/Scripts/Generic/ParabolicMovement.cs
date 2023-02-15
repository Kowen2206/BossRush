using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class ParabolicMovement : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed = 5f, upForce = 2;
    Rigidbody2D _rigid;
    float _timer = 0, _maxYvalue = 7, _minYvalue = -7, _finalYvalue = 0;
    [SerializeField] bool _moveOnStart = false;
    bool _isMoving, _isFalling;
    [SerializeField] ThrowDirections _throwDirection;
    [SerializeField] int _backOrderInLayer, _frontOrderInLayer;
    SpriteRenderer _spriteRenderer;
    [SerializeField] float _minFallPosition, _maxFallPosition;
    public static int _lastDirectionIndex = -1;

    
    public float MinFallPosition{
        get => _minFallPosition;
        set => _minFallPosition = value;
    }

    public float MaxFallPosition{
        get => _maxFallPosition;
        set => _maxFallPosition = value;
    }

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_moveOnStart) _isMoving = true;
    }

    //La trayectoría se obtiene de un constante movimiento en el eje x, ya sea positivo o negativo, pero en una sola dirección y otra fuerza constante hacía abajo que de apoco
    //va debilitando la fuerza inicial hacia arriba.

    public void StartMove()
    {
        _isMoving = true;
    }

    void Update()
    {
        if(_timer <= upForce && _isMoving)
        {
            Throw();
            _timer += Time.deltaTime;
            
        }
        else
        if(_timer > upForce && !_isFalling)
        {
            _isMoving = false;
            _isFalling = true;
            SetFallValue();
        }

        if(_isFalling)
        {
            if(transform.position.z > _finalYvalue)
            {
                _rigid.simulated = false;
            }
        }
    }

    void Throw()
    {
        switch (_throwDirection)
        {
            case ThrowDirections.Left:
            _rigid.AddForce((Vector2.up * 4 + Vector2.left) * Time.deltaTime * _speed);
                break;
            case ThrowDirections.Right:
            _rigid.AddForce((Vector2.up * 4 + Vector2.right) * Time.deltaTime * _speed);
                break;
            case ThrowDirections.InFront:
            _spriteRenderer.sortingOrder = _frontOrderInLayer;
            _rigid.AddForce(Vector2.up * 4 * Time.deltaTime * _speed);
                break;
            case ThrowDirections.Back:
            _spriteRenderer.sortingOrder = _backOrderInLayer;
            _rigid.AddForce(Vector2.up * 4 * Time.deltaTime * _speed);
                break;
            default:
                break;
        }
    }

    void SetFallValue()
    {
        _finalYvalue = Random.Range(_minFallPosition, _maxFallPosition + 1);
    }

    public ThrowDirections GetRandomDirection()
    {
        int randomIndex = Random.Range(0, 4);
        while(_lastDirectionIndex == randomIndex)
        {
            randomIndex = Random.Range(0, 4);
        }
        
        switch (randomIndex)
        {
            case 0:
                return ThrowDirections.Left;
            case 1:
                return ThrowDirections.Right;
            case 2:
                return ThrowDirections.Back;
            case 3:
                return ThrowDirections.InFront;
            default:
                return ThrowDirections.Right;
        }
        
    }

    public enum ThrowDirections
    {
        Left,
        Right,
        Back,
        InFront
    }

}
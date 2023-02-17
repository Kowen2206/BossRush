using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class ParabolicMovement : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed = 5f, _upTime = 2, _upForce = 4, _sidesForce = 1;
    Rigidbody2D _rigid;
    float _timer = 0, _maxYvalue = 7, _minYvalue = -7, _finalYvalue = 0;
    [SerializeField] bool _moveOnStart = false, _useRandomDirection;
    bool _isMoving, _isFalling;
    [SerializeField] ThrowDirections _throwDirection;
    [SerializeField] int _backOrderInLayer, _frontOrderInLayer;
    SpriteRenderer _spriteRenderer;
    [SerializeField] float _minFallPosition, _maxFallPosition;
    float _limitsOfsset;
    public static int _lastDirectionIndex = -1;
    Vector3 _currentTrayectory,_lastPosition, _initialPosition;
    public float bottomOffset = 0;
    public delegate void OnPlaceAction(Vector3 position);
    public OnPlaceAction onGetInFloor;

    public float LimitsOffset
    {
        get => _limitsOfsset;
        set => _limitsOfsset = value;
    }
    public float MinFallPosition
    {
        get => _minFallPosition;
        set => _minFallPosition = value;
    }

    public float MaxFallPosition{
        get => _maxFallPosition;
        set => _maxFallPosition = value;
    }

    public float SidesForce
    {
        get => _sidesForce;
        set => _sidesForce = value;
    }

    public ThrowDirections Direction
    {
        get => _throwDirection;
    }
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _initialPosition = transform.position;
        if(_moveOnStart) 
            _isMoving = true;
    }

    public void StartMove()
    {
        if(_useRandomDirection) _throwDirection = GetRandomDirection();
        CalculateMinAndMaxFloorHeigth();
        _isMoving = true;
    }

    void Update()
    {
        _currentTrayectory = transform.position - _lastPosition;
        _currentTrayectory.Normalize();
        if(_timer <= _upTime && _isMoving)
        {
            Throw();
            _timer += Time.deltaTime;
            
        }
        else
        if(_timer > _upTime && !_isFalling)
        {
            SetFallValue();
            _isMoving = false;
            _isFalling = true;
        }

        if(_isFalling && _currentTrayectory.y < 0)
        {
            if(transform.position.y < _finalYvalue)
            {
                _rigid.simulated = false;
                onGetInFloor?.Invoke(transform.position);
                GetComponent<DestroyObject>().DestroyWithDelay(.5f);
            }
        }
        _lastPosition = transform.position;
    }

    void Throw()
    {
        switch (_throwDirection)
        {
            case ThrowDirections.Left:
            _rigid.AddForce((Vector2.up * _upForce + Vector2.left * _sidesForce) * Time.deltaTime * _speed);
                break;
            case ThrowDirections.Right:
            _rigid.AddForce((Vector2.up * _upForce + Vector2.right * _sidesForce) * Time.deltaTime * _speed);
                break;
            case ThrowDirections.InFront:
            _spriteRenderer.sortingOrder = _frontOrderInLayer;
            _rigid.AddForce(Vector2.up * _upForce * Time.deltaTime * _speed);
                break;
            case ThrowDirections.Back:
            _spriteRenderer.sortingOrder = _backOrderInLayer;
            _rigid.AddForce(Vector2.up * _upForce * Time.deltaTime * _speed);
                break;
            default:
                break;
        }
    }

    void CalculateMinAndMaxFloorHeigth ()
    {
        if(_throwDirection == ThrowDirections.Back)
        {
            MaxFallPosition = LookForWalls._upWallPos.y - _limitsOfsset;
            MinFallPosition = transform.position.y + 1;
        }
        else
        if(_throwDirection == ThrowDirections.InFront)
        {
            MaxFallPosition = _initialPosition.y - bottomOffset;
            MinFallPosition = LookForWalls._downWallPos.y - _limitsOfsset;
        }
        else
        {
            MaxFallPosition = LookForWalls._upWallPos.y - _limitsOfsset;
            MinFallPosition = LookForWalls._downWallPos.y + _limitsOfsset;
        }
    }

    public void SetFallValue()
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
        _lastDirectionIndex = randomIndex;
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


}

public enum ThrowDirections
    {
        Left,
        Right,
        Back,
        InFront
    }
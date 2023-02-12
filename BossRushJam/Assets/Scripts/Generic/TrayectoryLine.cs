using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class TrayectoryLine : MonoBehaviour
{
    [SerializeField] float _growSpeed, _maxSize;
    [SerializeField] string _targetId;
    [SerializeField] bool _growInYaxis = false, _switchGrowingDirection, _drawLineOnStart, _ignoreCollisionObjects = true, _ignoreTarget;
    float _currentSize, initialSize; 
    public bool _isGrowing = false;
    BoxCollider2D  _lineCollider;
    SpriteRenderer _sprite;

    public bool GrowInYaxis {set => _growInYaxis = value;}

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _lineCollider = GetComponent<BoxCollider2D>();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void OnEnable()
    {
        
        if(_drawLineOnStart) 
        {
            StartGrowing();
            
            
        }
    }

    void OnDisable()
    {
       _sprite.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        initialSize = _growInYaxis? transform.localScale.y : transform.localScale.x;
        _lineCollider.isTrigger = true;
        
    }

    public void StartGrowing()
    {
        _currentSize = initialSize;
        _sprite.enabled = true;
        _isGrowing = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(_isGrowing)
        {
            if(!_switchGrowingDirection)
                _currentSize += _growSpeed * Time.deltaTime;
            else
                _currentSize -= _growSpeed * Time.deltaTime;
            
            transform.localScale = _growInYaxis? new Vector3(transform.localScale.x, _currentSize, 1) : new Vector3(_currentSize, transform.localScale.y, 1) ;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_targetId) && !_ignoreTarget)
        {
            _isGrowing = false;
        }
            else
        if(!_ignoreCollisionObjects && other.CompareTag("collisionObject"))
        {
            _isGrowing = false;
        }
    }
}

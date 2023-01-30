using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTowardsTarget : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] bool _toggleDirection, _lookingAtTarget = true;
    [SerializeField] bool  _lookingAtMoveDirection = false;
    Vector3 _lastPosition;
    SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public bool LookingAtTarget
    {
        get => _lookingAtTarget;
        set {
                _lookingAtTarget = value;
            }
    }

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    // Update is called once per frame
    void Update()
    {
        if(_lookingAtTarget)
        {
            CalculateFlipDirection(transform.position, _target.position);
        }
        else
        {
            CalculateFlipDirection(_lastPosition, transform.position);
        }
    }

    void CalculateFlipDirection(Vector3 pointA, Vector3 pointB)
    {
        if(pointA.x - pointB.x > 0)
                 _spriteRenderer.flipX = _toggleDirection? true : false;
            else
                _spriteRenderer.flipX = _toggleDirection? false : true;
    }
}

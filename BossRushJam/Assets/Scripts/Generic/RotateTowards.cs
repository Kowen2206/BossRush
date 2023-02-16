using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _rotationSpeed;
    [SerializeField] bool _updateRotationOnTargetMoves;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        if(!_target) _target = GameObject.FindGameObjectWithTag("Player").transform;
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        if(_updateRotationOnTargetMoves) Rotate();
        
    }

    void Rotate()
    {
        direction = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}

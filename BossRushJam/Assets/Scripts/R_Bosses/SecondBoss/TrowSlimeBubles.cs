using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TrowSlimeBubles : MonoBehaviour
{
    [SerializeField] GameObject _bublePrefab, _miniBoss;
    [SerializeField] int _bublesCount = 3;
    [SerializeField] float _bublesTimeDelay, _wallsOffset;
    [SerializeField] UnityEvent OnFinishThrowBubles;
    public bool _throwBubles;
    BoxCollider2D _collider;
    float _colliderHeight;
    int initialBublesCount;

    void Update()
    {
        if(_throwBubles)
        {
            _throwBubles = false;
            StartCoroutine(createBublesSlimeRoutine());
        }
    }

    void Start()
    {
        initialBublesCount = _bublesCount;
        _collider = GetComponent<BoxCollider2D>();
        _colliderHeight = _collider.size.y;
    }

    public void InstantiateMiniBoss(Vector3 position)
    {
        Instantiate(_miniBoss, position, Quaternion.identity);
        
    }

    IEnumerator createBublesSlimeRoutine()
    {
        while(_bublesCount > 0)
        {
            Vector3 bubleStartPosition = new Vector3(transform.position.x, transform.position.y + _colliderHeight, 1);
            GameObject buble = Instantiate(_bublePrefab, bubleStartPosition, Quaternion.identity);
            buble.name = buble.name + _bublesCount;
            ParabolicMovement bubleMovement =  buble.GetComponent<ParabolicMovement>();
            bubleMovement.LimitsOffset = _wallsOffset;
            bubleMovement.bottomOffset = _colliderHeight;
            bubleMovement.SidesForce =  bubleMovement.SidesForce + bubleMovement.SidesForce * .5f;
            bubleMovement.onGetInFloor += InstantiateMiniBoss;
            //Random.Range(bubleMovement.SidesForce, bubleMovement.SidesForce + bubleMovement.SidesForce * .5f);
            bubleMovement.StartMove();
            yield return new WaitForSeconds(_bublesTimeDelay);
            _bublesCount--;
        }
        _bublesCount = initialBublesCount;
        OnFinishThrowBubles?.Invoke();
    }
    
}

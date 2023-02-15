using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TrowSlimeBubles : MonoBehaviour
{
    GameObject _bublePrefab;
    [SerializeField] int _bublesCount = 3;
    [SerializeField] float _bublesTimeDelay;
    BoxCollider2D _collider;
    float _colliderHeight;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _colliderHeight = _collider.size.y;
    }

    void CalculateMinAndMaxHeigth (ParabolicMovement.ThrowDirections direction, ParabolicMovement buble)
    {
        switch (direction)
        {
            case ParabolicMovement.ThrowDirections.Left:
                //Todo: With raycast2D get coordinates of walls and avoid it goes out of scenary (in all directions)
                break;
            case ParabolicMovement.ThrowDirections.Right:
                break;
            case ParabolicMovement.ThrowDirections.InFront:
                break;
            case ParabolicMovement.ThrowDirections.Back:
                break;
            default:
                break;
        }
        
    }

    IEnumerator createBublesSlime()
    {
        while(_bublesCount > 0)
        {
            Vector3 bubleStartPosition = new Vector3(transform.position.x, transform.position.y + _colliderHeight, 1);
            GameObject buble = Instantiate(_bublePrefab, bubleStartPosition, Quaternion.identity);
            ParabolicMovement bubleMovement =  buble.GetComponent<ParabolicMovement>();
            ParabolicMovement.ThrowDirections direction =  bubleMovement.GetRandomDirection();
            yield return new WaitForSeconds(_bublesTimeDelay);
        }
    }

}

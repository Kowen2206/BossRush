using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForWalls : MonoBehaviour
{
    public static Vector3 _leftWallPos, _rightWallPos, _upWallPos, _downWallPos;
    Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GetWallsPositions();
    }

    void GetWallsPositions()
    {
        _leftWallPos = ThrowRay(Vector3.left);
        _rightWallPos = ThrowRay(Vector3.right);
        _upWallPos = ThrowRay(Vector3.up);
        _downWallPos = ThrowRay(Vector3.down);
    }

    Vector3 ThrowRay(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100);
        if (hit.collider != null)
        {
            return hit.collider.transform.position;
        }
        else 
        return Vector3.zero;
    }
}

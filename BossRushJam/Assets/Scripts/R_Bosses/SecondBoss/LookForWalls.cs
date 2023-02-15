using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForWalls : MonoBehaviour
{
    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;      // Force reduction proportional to speed (reduces bouncing).
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
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100);
        // If it hits something...
        if (hit.collider != null)
        {
            return hit.collider.transform.position;
        }
        else 
        return Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateColliderBetweenPoints : MonoBehaviour
{
    [SerializeField] float _colliderWidth;
    BoxCollider2D _collider;

    public void ConfigureCollider(GameObject newCollider)
    {
        
        newCollider.AddComponent<BoxCollider2D>();
        _collider = newCollider.GetComponent<BoxCollider2D>();
        _collider.size = new Vector2(1f, 1f);
        _collider.offset = new Vector2(_collider.size.y/2, 0);
        _collider.isTrigger = true;
    }

    public GameObject CreateCollider(Vector3 pointA, Vector3 pointB)
    {
        GameObject newCollider = new GameObject();
        ConfigureCollider(newCollider);
        newCollider.transform.position = pointA;
        newCollider.transform.localScale = new Vector3(Vector3.Distance(pointA, pointB), 1, 1);
        Vector3 direction = (pointB - pointA).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newCollider.transform.rotation = rotation;
        newCollider.AddComponent<DestroyObject>();
        return newCollider;
    }
}

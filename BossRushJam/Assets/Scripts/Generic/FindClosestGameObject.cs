using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestGameObject : MonoBehaviour
{
    [SerializeField] List<GameObject> _objects;

    public List<GameObject> Objects{
        get => _objects; set => _objects = value;
    }

    public GameObject FindClosest(Vector3 position)
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in _objects)
        {
            float distance = Vector3.Distance(position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }
}

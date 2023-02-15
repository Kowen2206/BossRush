using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointGenerator : MonoBehaviour
{
    [SerializeField] float _maxDistance = 10f;
    [SerializeField] int _pointCount = 10;

 List<Vector2> _randomPoints;

 void Start()
    {
        _randomPoints = new List<Vector2>();
        for (int i = 0; i < _pointCount; i++)
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float distance = Random.Range(0, _maxDistance);
            Vector2 randomPoint = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            _randomPoints.Add(randomPoint);
        }
    }

 void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxDistance);
        foreach (Vector2 randomPoint in _randomPoints)
        {
            Gizmos.DrawSphere(randomPoint, 0.1f);
        }
    }
}
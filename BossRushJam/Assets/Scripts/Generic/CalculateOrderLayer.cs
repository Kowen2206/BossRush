using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateOrderLayer : MonoBehaviour
{
    List<GameObject> _targets = new List<GameObject>();
    [SerializeField] string[] _targetsId;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] float _minTargetDistance = 2;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if(Player) _targets.Add(Player);
        if(Boss) _targets.Add(Boss);
        UpdateTarges();
    }

    void UpdateTarges()
    {
        foreach (string targetId in _targetsId)
        {
            GameObject[] target = GameObject.FindGameObjectsWithTag(targetId);
            if(target.Length > 0) _targets.AddRange(target);
        }
    }

    void Update()
    {
        SetLayerPosition();
    }

    void SetLayerPosition()
    {
        foreach (GameObject target in _targets)
        {
            if( Vector3.Distance(target.transform.position, transform.position) <= _minTargetDistance)
            {
                if( transform.position.y < target.transform.position.y)
                {
                _sprite.sortingOrder = 1 + target.GetComponent<SpriteRenderer>().sortingOrder;
                }
                else
                _sprite.sortingOrder = target.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
        }
    }
}

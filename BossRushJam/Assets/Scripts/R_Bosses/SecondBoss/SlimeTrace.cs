using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SlimeTrace : MonoBehaviour
{
    LineRenderer _line;
    Vector3 _lastSlimeTrace;
    

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        _line.SetPosition(0, transform.position);
    }

    void Update()
    {

    }

}

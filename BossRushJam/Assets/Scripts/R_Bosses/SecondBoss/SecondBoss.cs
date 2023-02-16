using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent), typeof(Proyectil))]
public class SecondBoss : MonoBehaviour
{
    NavMeshAgent _agent;
    Proyectil _bossImpulse;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void Punch()
    {
    }
}

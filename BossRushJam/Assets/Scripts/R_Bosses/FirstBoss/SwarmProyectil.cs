using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine;

public class SwarmProyectil : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] float _swarmBackSpeed;
    private static GameObject _restPoint;
    public string lastPositionTargetId = "Boss";
    [SerializeField] Animator _animator;
    bool collided;

    void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.enabled = false;
        _restPoint = GameObject.FindGameObjectWithTag(lastPositionTargetId);
    }

    public void GetBackToInitialPoint()
    {
        if(!_agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
            {
                _agent.Warp(hit.position);
            }
        }
        _agent.enabled = true;
        _agent.speed = _swarmBackSpeed;
        _agent.stoppingDistance = 0;
        _agent.SetDestination(_restPoint.transform.position);
        collided = true;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag(lastPositionTargetId) || other.CompareTag("SwarmProyectil")) && collided)
        {
            _animator.SetTrigger("Die");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

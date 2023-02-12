using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowSwarmOfBees : MonoBehaviour
{
    NavMeshAgent _agent;
    GameObject _player;
    Vector3 _lastPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.SetDestination(_player.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
            _agent.SetDestination(_player.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MiniBossCopy : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] Transform _player;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.position);
    }
}

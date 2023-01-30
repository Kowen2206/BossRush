using System.Collections;

using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
//Todo: revisar mis pendientes a las 10
//Todo: usar shortAttack o quitarlo
public class FirstBoss : Boss
{
    [Header("Jump configuration")]
    [SerializeField] float _jumpDelay = 0.5f;
    [SerializeField] float _jumpSpeed = 5;
    [Header("Spinning configuration")]
    [SerializeField] float _spinningAttackSpeed = 12;
    [SerializeField] float  _SpinningAttackAttackDuration = 17;
    [Header("Tackle configuration")]
    [SerializeField] float _tackleSpeed = 20;
    [Header("Swarm configuration")]
    [SerializeField] GameObject _swarm;
    [SerializeField] int _swarmQuantity;
    [SerializeField] float _swarmDelay;
    [Header("BurstOfClaws configuration")]
    [SerializeField] GameObject _burstOfClaw;
    [SerializeField] int _clawsQuantity;
    [SerializeField] float _ClawsDelay;
    [Header("ThrowRocks configuration")]
    [SerializeField] GameObject _rock;
    [SerializeField] int _rocksQuantity;
    [SerializeField] float _rockDelay;
    void Start()
    {
        InitialiceBoos();
    }

    //Select a random attack from  the attack List
    override public void  ChoseRandomAttack()
    {
        //string nextAttack = attacksFase1[UnityEngine.Random.Range(0, attacksFase1.Count)];
        string nextAttack = GetAttackNameByPhase();
        Debug.Log("nextAttack" + nextAttack);
        switch (nextAttack)
        {
            //Try to 
            case "jump":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, Jump, 2, Random.Range(1, 5));
                StartAttack();
                break;
            case "throwRocks":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, ThrowRocks, 3f, 1);
                StartAttack();
                break;
            case "spinningAttack":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, SpinningAttack, 0, 1);
                StartAttack();
                break;
            case "tackle":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, Tackle, 2);
                StartAttack();
                break;
            case "swarmOfBees":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, SpawnSwarm, 3f, 1);
                StartAttack();
                break;
            case "burstOfClaws":
                ConfigureAttack(new OnPlaceAction[] {BossAttackDelay}, BurstOfClaws, 3f, 1);
                    StartAttack();
                break;
            case "beeShield":
                break;
           //Todo: case "verticalHorizontalBees":
            default:
                break;
        }
    }

    void Update()
    { 
        if(_agent.enabled)
        CheckBossPath();
    }

    

    void Tackle()
    {
       _agent.enabled = false;
       _lastPlayerPosition = _player.transform.position;
       StartCoroutine(TackleRoutine(_tackleSpeed));
    }

    void ThrowRocks()
    {
        _agent.enabled = false;
        StartCoroutine(ThrowProyectilRoutine(_rock ,_rocksQuantity, _rockDelay));
    }

    void SpawnSwarm()
    {
        StartCoroutine(ThrowProyectilRoutine(_swarm, _swarmQuantity, _swarmDelay));
       
    }

    void BurstOfClaws()
    {
        _agent.enabled = false;
        StartCoroutine(ThrowProyectilRoutine(_burstOfClaw, _clawsQuantity, _ClawsDelay));
    }
    
    void SpinningAttack()
    {
        _isCheckingPhat = false;
        StartCoroutine(FollowPlayerRoutine(_SpinningAttackAttackDuration));
    }

    void Jump()
    {
        _isCheckingPhat = false;
        _agent.speed = _jumpSpeed;
        _agent.stoppingDistance = 0;
        StartCoroutine(FollowPlayerWithStopsRoutine(_jumpDelay));
    }
}

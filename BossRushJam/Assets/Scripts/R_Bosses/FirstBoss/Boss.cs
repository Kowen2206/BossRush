using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] protected  NavMeshAgent _agent;
    //TargetPlace represents the coordinates in the world of the place that the boss have to arrive.
    //_keyPointsCoordinatesParent is a gameObject parent of a points in the map, they are used to select random positions o the map
    [SerializeField] protected Transform _keyPointsCoordinatesParent, _proyectilSpawnPoint, _bossRestPoint;
    [SerializeField] protected GameObject _player;
    protected bool _isDie, _makeDamageOnTouch;
    //Todo: _defaultSpeed will be use to the speed of the boss depending on phase or attack
    [SerializeField] protected float _defaultSpeed, _damageOnTouch, _actionDelay = 10;
    protected Vector3 _lastPlayerPosition, lastAttackPreparationPosition = Vector3.zero;
    //In order to avoid conflicts with _agent when is disable, _isCheckingPath avoid check the _agent position in update method.
    public static bool _isCheckingPhat = false;
    protected int _currentPhase = 0, _remainingAttack = 0;
    protected delegate void ExecutAttack();
    protected ExecutAttack actionDelegate;
    protected delegate void OnPlaceAction();
    protected OnPlaceAction onPlaceActionDelegate;
    [SerializeField] protected PhaseAttacks[] _attacksList;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    protected string _lastAttack = "";
    [SerializeField] protected UnityEvent OnDie, OnPrepareToAttack, OnFinisAttackRound, OnFinishAttack, OnFinishPhase;
    
    //This method must be called in the Start Method of every child
    protected void InitialiceBoos()
    {
        ChoseRandomAttack();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    //This method will be a virtual method that must be override on the children clases
    public virtual void ChoseRandomAttack()
    {
        
    }

    protected string GetAttackNameByPhase()
    {
        PhaseAttacks attack = _attacksList[UnityEngine.Random.Range(0, _attacksList.Length)];
        while(attack.Phase != _currentPhase && attack.name == _lastAttack)
        {
            attack = _attacksList[UnityEngine.Random.Range(0, _attacksList.Length)];
        }
        return attack.name;
    }    

     //The ConfigureAttackFunction mekes a set up for the boss to execute an attack,
    /* 
        -   onPlaceAction: Its a Array of the functions that must be executed once the boss get in the place where he will start his attack.
        -   attackFunction: It's the corresponding attack that the boss must execute.
        -   attacDelay: The time that the boss will take to attack after gets in the corresponding place.
        -   attackCount: number of times that the boss will repeat the attack.
    */
    protected void ConfigureAttack(OnPlaceAction[] onPlaceFunctions, ExecutAttack attackFunction, float attackDelay = 2, int attackCount = 4)
    {
        actionDelegate = attackFunction;
        foreach (OnPlaceAction func in onPlaceFunctions)
        {
            onPlaceActionDelegate += func;
        }
        _actionDelay = attackDelay;
        _remainingAttack = attackCount;
    }

    protected void StartAttack(bool shortAttack = false)
    {
        _isCheckingPhat = true;
        _agent.enabled = true;
        if(!shortAttack) SetDistanceAttackPlace(); else SetShortAttackPlace();
    }

    //If is necesary, Boss will set a specific distance with the player in order to prepare to attack 
    //Boos can go infront player to make short distance attacks or posisionate far in order to make a distance attack 
    protected void SetShortAttackPlace(float playerDistance = 0)
    {
            _agent.stoppingDistance = playerDistance;
            _agent.SetDestination(GetRandomMapCoordinate(false));
    }

    protected void SetDistanceAttackPlace(float speed = 0)
    {
        
        if(speed == 0) speed = _defaultSpeed;
        _isCheckingPhat = true;
        _agent.SetDestination(GetRandomMapCoordinate(true));
    }


    

    protected void CheckBossPath()
    {
        if (_agent.remainingDistance - _agent.stoppingDistance < 0.1f && !_agent.pathPending && _isCheckingPhat)
        {
            _isCheckingPhat = false;
            
            onPlaceActionDelegate?.Invoke();
        }
    }

    
    protected Vector3 GetRandomMapCoordinate(bool farOfPlayer)
    {
        float biggestDistance = 0;
        float minorDistance  = 1000;
        Transform targetPlace = _keyPointsCoordinatesParent.transform.GetChild(0);
        foreach (Transform coordinate in _keyPointsCoordinatesParent)
        {
            if(farOfPlayer)
                if(Vector3.Distance(coordinate.position, _player.transform.position) > biggestDistance && coordinate.position != transform.position
                
                && lastAttackPreparationPosition != coordinate.position)
                {
                        biggestDistance = Vector3.Distance(coordinate.position, _player.transform.position);
                        targetPlace = coordinate;
                }
            else
                if(Vector3.Distance(coordinate.position, _player.transform.position) < minorDistance && coordinate.position != transform.position
                
                && lastAttackPreparationPosition != coordinate.position)
                {
                        minorDistance = Vector3.Distance(coordinate.position, _player.transform.position);
                        targetPlace = coordinate;
                }
        }
        lastAttackPreparationPosition = targetPlace.transform.position;
        return targetPlace.transform.position;
    }

    protected void BossAttackDelay() => StartCoroutine(BossAttackDelayRoutine());

    IEnumerator BossAttackDelayRoutine()
    {
        yield return new WaitForSeconds(_actionDelay);
        actionDelegate?.Invoke();
    }

    //This metod will be called by the event OnReciveDamage in HealtController.
    protected void CheckPhase(float healtPercentage)
    {
        if(healtPercentage < 70)  _currentPhase = 1;
        if(healtPercentage < 40) _currentPhase = 2;
    }

    //If is necesary, Boss will set a specific distance with the player in order to prepare to attack 
    protected void setRestPlace()
    {
        _agent.SetDestination(_bossRestPoint.position);
    }

    protected void CountAttacks()
    {
        StopAllCoroutines();
        _agent.speed = _defaultSpeed;
        _remainingAttack --;
        if(_remainingAttack > 0)
        {
            StartAttack();
        }
        else
        {
            onPlaceActionDelegate = ChoseRandomAttack;
            _agent.enabled = true;
            SetDistanceAttackPlace();
        }
    }

    protected IEnumerator TackleRoutine(float tackleSpeed)
    {
        //Todo: Agregar que al chocar contra ciertas estructuras u bojetos, el boss sea stuned por un x tiempo. 
        while(transform.position != _lastPlayerPosition)
        {
            Debug.Log("ExecutingWhile");
            transform.position = Vector3.MoveTowards(transform.position, _lastPlayerPosition, Time.deltaTime * tackleSpeed);
             yield return null;
        }

        Debug.Log("Finish Tackle"); 
        OnFinishAttack?.Invoke();
        CountAttacks();
    }

    protected IEnumerator ThrowProyectilRoutine( GameObject proyectil, int proyectilCount, float proyectilDelay = 1.5f)
    {
        while(proyectilCount > 0)
        {
            
            Instantiate(proyectil, _proyectilSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(proyectilDelay);
            
            proyectilCount--;
        }
        OnFinishAttack?.Invoke();
        CountAttacks();
    }

    protected IEnumerator FollowPlayerRoutine(float followDuration)
    {
        _spriteRenderer.color = new Color(255,0,0);
        while(followDuration > 0)
        {
            yield return new WaitForSeconds(1);
            _agent.SetDestination(_player.transform.position);
            followDuration--;
        }
        _spriteRenderer.color = new Color(255,255,255);
        CountAttacks();
    }

    protected IEnumerator FollowPlayerWithStopsRoutine(float stopDelay)
    {
        _lastPlayerPosition = _player.transform.position;
        _agent.SetDestination(_lastPlayerPosition);
        
        while(transform.position != _lastPlayerPosition)
        {
            yield return null;
        }
        yield return new WaitForSeconds(stopDelay);
        CountAttacks();
    }

    [Serializable]
    public struct PhaseAttacks
    {
       public string name;
       public int Phase;
    }
}

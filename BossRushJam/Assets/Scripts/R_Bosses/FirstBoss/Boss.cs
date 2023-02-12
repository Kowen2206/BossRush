using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    //Todo: Crear función para detener attaques en seco.
    //Configurar velocidad en función del ataque
    //Agregar sonidos
    [SerializeField] protected  NavMeshAgent _agent;
    //TargetPlace represents the coordinates in the world where the boss have to arrive.
    //_keyPointsCoordinatesParent is a gameObject parent of a especific points in the map, they are used to select random positions on the map
    [SerializeField] protected Transform _keyPointsCoordinatesParent, _proyectilSpawnPoint, _bossRestPoint;
    [SerializeField] protected GameObject _player;
    public bool proofOneAttack, seriousDamage;
    protected bool _isDie, _makeDamage;
    //Todo: _defaultSpeed will be use to the speed of the boss depending on phase or attack
    [SerializeField] protected float _defaultSpeed, _currentSpeed, _damageOnTouch, _actionDelay = 10, _damageRecoverTime;
    protected Vector3 _lastPlayerPosition, lastAttackPreparationPosition = Vector3.zero;
    //In order to avoid conflicts with _agent when is disable, _isCheckingPath avoid check the _agent position in update method.
    public static bool _isCheckingPhat = false;
    [SerializeField] protected int _currentPhase = 0, _remainingAttack = 0;
    protected delegate void ExecutAttack();
    protected ExecutAttack AttackDelegate, FinishAttackDelegate;
    protected delegate void OnPlaceAction();
    protected OnPlaceAction onPlaceActionDelegate;
    [SerializeField] protected PhaseAttacks[] _attacksList;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
   [SerializeField] protected string _lastAttack = "";
    [SerializeField] protected UnityEvent OnDie, OnFinishAttack, OnWalk, OnRun, OnStopWalk, OnStopRun, OnTackle, OnStartJump;
    [SerializeField] protected UnityEvent OnThrowProyectil, OnFinishThrowProyectil, OnStopInFollow, OnContinueInFollow;
    [SerializeField] protected AnimationsBoss _animationBoss;
    [SerializeField] protected List<GameObject> _currentProyectils = new List<GameObject>();
    TrayectoryLine _trayectoryLine;
    [SerializeField] GameObject _line;

    
    //This method must be called in the Start Method of every child
    protected void InitialiceBoos()
    {
        _animationBoss = GetComponent<AnimationsBoss>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        ChoseRandomAttack();
        _trayectoryLine =  _line.GetComponent<TrayectoryLine>();
        _trayectoryLine.enabled = false;
    }

    //This method will be a virtual method that must be override on the children clases
    public virtual void ChoseRandomAttack()
    {
        
    }

    protected string GetAttackNameByPhase()
    {
        PhaseAttacks attack = _attacksList[UnityEngine.Random.Range(0, _attacksList.Length)];
        while(attack.Phase != _currentPhase || attack.name == _lastAttack)
        {
            attack = _attacksList[UnityEngine.Random.Range(0, _attacksList.Length)];
        }
        if(!proofOneAttack) _lastAttack = attack.name;
        return attack.name;
    }    

     //The ConfigureAttackFunction mekes a set up for the boss to execute an attack,
    /* 
        -   onPlaceAction: Its a Array of the functions that must be executed once the boss get in the place where he will start his attack.
        -   attackFunction: It's the corresponding attack that the boss must execute.
        -   attacDelay: The time that the boss will take to attack after gets in the corresponding place.
        -   attackCount: number of times that the boss will repeat the attack.
    */
    protected void ConfigureAttack(OnPlaceAction onPlaceFunction, ExecutAttack attackFunction, float attackDelay = 2, int attackCount = 4)
    {
        AttackDelegate = attackFunction;
        onPlaceActionDelegate = onPlaceFunction;
        _actionDelay = attackDelay;
        _remainingAttack = attackCount;
    }

    protected void StartAttack(bool shortAttack = false)
    {
        _isCheckingPhat = true;
        _agent.enabled = true;
        if(!shortAttack) SetDistanceAttackPlace(); else SetShortAttackPlace();
    }

    public void ReciveSeriousDamage()
    {
        StopAllCoroutines();
        _agent.enabled = false;
        _remainingAttack = 0;
        FinishAttackDelegate = null;
        onPlaceActionDelegate = null;
        StartCoroutine(RecoverOfDamageRoutine(_damageRecoverTime));

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
        OnWalk?.Invoke();
    }

    protected void CheckBossPath()
    {
        if (_agent.remainingDistance - _agent.stoppingDistance < 0.1f && !_agent.pathPending && _isCheckingPhat)
        {
            _isCheckingPhat = false;
            StopAllCoroutines();
            OnStopWalk?.Invoke();
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
        AttackDelegate?.Invoke();
        
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
            Debug.Log("ShouldStartNextAttack");
            FinishAttackDelegate = null;
            onPlaceActionDelegate = null;
            onPlaceActionDelegate = ChoseRandomAttack;
            _agent.enabled = true;
            SetDistanceAttackPlace();
        }
    }

    protected IEnumerator TackleRoutine(float tackleSpeed, float tackleDelay)
    {
        OnTackle?.Invoke();
        yield return new WaitForSeconds(tackleDelay);
        _lastPlayerPosition = _player.transform.position;
        //Todo: Agregar que al chocar contra ciertas estructuras u bojetos, el boss sea stuned por un x tiempo. 
        while(transform.position != _lastPlayerPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lastPlayerPosition, Time.deltaTime * tackleSpeed);
            yield return null;
        } 
        OnFinishAttack?.Invoke();
        CountAttacks();
    }

    protected IEnumerator ThrowProyectilRoutine( GameObject proyectil, int proyectilCount, float proyectilDelay = 1.5f)
    {
        OnThrowProyectil?.Invoke();
        while(proyectilCount > 0)
        {
            yield return new WaitForSeconds(proyectilDelay);
            Instantiate(proyectil, _proyectilSpawnPoint.position, Quaternion.identity);
            proyectilCount--;
        }
        OnFinishAttack?.Invoke();
        CountAttacks();
    }

    protected IEnumerator RecoverOfDamageRoutine(float recoverTime)
    {
        _animationBoss.SeriousDamageAnim();
        yield return new WaitForSeconds(recoverTime);
        _animationBoss.RecoverForDamage(true);
        StartCoroutine(WaitUnitlFinishAnimationRoutine("FinishRecover", true));
    }

    protected IEnumerator FollowPlayerRoutine(float followDuration)
    {
        while(followDuration > 0)
        {
            yield return new WaitForSeconds(1);
            _agent.SetDestination(_player.transform.position);
            followDuration--;
        }
        OnFinishAttack?.Invoke();
        FinishAttackDelegate?.Invoke();
        CountAttacks();
    }

    protected IEnumerator FollowPlayerWithStopsRoutine(float stopDelay, int stopsQuantity, float followSpeed)
    {
        _spriteRenderer.color = new Color(255,0,0);
        BoxCollider2D playerCollider = _player.GetComponent<BoxCollider2D>();
        Proyectil BossProyectil = GetComponent<Proyectil>();
        _agent.enabled = false;
        while(stopsQuantity > 0)
        {
            OnContinueInFollow?.Invoke();
            yield return new WaitForSeconds(stopDelay);
           // transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * followSpeed);
            BossProyectil.ProyectilSpeed = followSpeed;
            HideTrayectoryLine();
            BossProyectil.StartShoot();
            yield return new WaitUntil( () => BossProyectil.IsInTargetPosition);
            //Todo: activar daño cuando ocurre la animación de caida con animation events
            OnStopInFollow?.Invoke();
            yield return new WaitForSeconds(_animationBoss.GetAnimationLength("FinishJump"));
            stopsQuantity--;
        }
        OnFinishAttack?.Invoke();
        CountAttacks();
    }

    protected IEnumerator WaitUnitlFinishAnimationRoutine(string animationName, bool countAttack = true)
    {
        yield return new WaitForSeconds(_animationBoss.GetAnimationLength(animationName));
        if(countAttack)
        CountAttacks();
    }

    public void InstantiateObject()
    {
        if(_currentProyectils.Count == 1)
        Instantiate(_currentProyectils[0], transform.position, Quaternion.identity);
        if(_currentProyectils.Count > 1)
        Instantiate(_currentProyectils[UnityEngine.Random.Range(0, _currentProyectils.Count)], transform.position, Quaternion.identity);
    }

    public void UseTrayectoryLine()
    {
        //_line.transform.position = new Vector3(transform.position.x + 0.76f, transform.position.y, 1);
        //_line.transform.localPosition = new Vector3(0, 0, 1);
        _trayectoryLine.enabled = true;
        
       // _trayectoryLine.StartGrowing();
    }

    public void HideTrayectoryLine()
    {
        _trayectoryLine.enabled = false;
    }

    [Serializable]
    public struct PhaseAttacks
    {
       public string name;
       public int Phase;
    }
}

using System.Collections;

using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
//Todo: AgregarMetodo para interrumpir ataques
// Agregar efectos de movimiento.
//Agregar animación de destrucción al ataque de piedras
//Agregar sonidos
public class FirstBoss : Boss
{
    [Header("Jump configuration")]
    [SerializeField] float _jumpDelay = 0.5f;
    [SerializeField] float _jumpSpeed = 5;
    [SerializeField] int _jumpCount = 5;
    [Header("Spinning configuration")]
    [SerializeField] float _spinningAttackSpeed = 12;
    [SerializeField] float  _SpinningAttackDuration = 17;
    [SerializeField] GameObject _spinningSwarm;
    [SerializeField] Transform _spinningSwarmPoint;
    GameObject _currentSpinningSwarm;
    [Header("Tackle configuration")]
    [SerializeField] float _tackleSpeed = 20;
    [SerializeField] float _tackleDelay = 0;
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
    [SerializeField] UnityEvent OnSpinningStarts, OnThrowRock, OnSpawnFollowSwarm, OnAttackClaws, OnInvokeBeeShield;
    [Header("BeeShield configuration")]
    [SerializeField] GameObject _beeShield;
    [Header("HorizontalVerticalAttack configuration")]
    [SerializeField] GameObject _horizontal, _vertical;
    [SerializeField] GameObject _swarmProyectil;
    void Start()
    {
        Application.targetFrameRate = 60;
        InitialiceBoos();
    }

    //Select a random attack from  the attack List
    override public void  ChoseRandomAttack()
    {
        string nextAttack = GetAttackNameByPhase();
        Debug.Log("NextAttack" + nextAttack);
        switch (nextAttack)
        {
            
            case "jump":
                ConfigureAttack(BossAttackDelay, Jump, .5f, 1);
                StartAttack();
                break;
            case "throwRocks":
                ConfigureAttack(BossAttackDelay, ThrowRocks, 3f, 1);
                StartAttack();
                break;
            case "spinningAttack":
                ConfigureAttack(BossAttackDelay, SpinningAttack, 0, 1);
                StartAttack();
                break;
            case "tackle":
                ConfigureAttack(BossAttackDelay, Tackle, 2);
                StartAttack();
                break;
            case "swarmOfBees":
                ConfigureAttack(BossAttackDelay, SpawnSwarm, 0f, 1);
                StartAttack();
                break;
            case "burstOfClaws":
                ConfigureAttack(BossAttackDelay, BurstOfClaws, 3f, 1);
                StartAttack();
                break;
            case "beeShield":
                ConfigureAttack(BeeShield, Jump, .5f, 1);
                StartAttack();
                break;
           case "verticalHorizontalBees":
                ConfigureAttack(BossAttackDelay, HorizontalVerticalBees, .5f, 1);
                StartAttack();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if(_agent.enabled)
        CheckBossPath();
        if(seriousDamage)
        {
            seriousDamage = false;
            ReciveSeriousDamage();
        }
    }

    

    void Tackle()
    {
       _agent.enabled = false;
       OnTackle?.Invoke();
       _tackleDelay = _animationBoss.GetAnimationLength("StartTackle");
       StartCoroutine(TackleRoutine(_tackleSpeed, _tackleDelay));
    }

    void ThrowRocks()
    {
        _agent.enabled = false;
        OnThrowRock?.Invoke();
        StartCoroutine(ThrowProyectilRoutine(_rock ,_rocksQuantity, _rockDelay));
    }

    void SpawnSwarm()
    {
        _currentProyectils.Clear();
        _currentProyectils.Add(_swarm);
        if(GameObject.FindGameObjectsWithTag("FollowSwarm").Length < 3)
            OnSpawnFollowSwarm?.Invoke();
            CountAttacks();
    }

    void BurstOfClaws()
    {
        _currentProyectils.Clear();
        _currentProyectils.Add(_burstOfClaw);
        OnAttackClaws?.Invoke();
        StartCoroutine(WaitUnitlFinishAnimationRoutine("ClawsAttack", true));
    }
    
    void SpinningAttack()
    {
        _isCheckingPhat = false;
        OnSpinningStarts?.Invoke();
        FinishAttackDelegate += FinishSwarm;
        _currentSpinningSwarm = Instantiate(_spinningSwarm, _spinningSwarmPoint.position, Quaternion.identity);
        _currentSpinningSwarm.transform.SetParent(transform);
        StartCoroutine(FollowPlayerRoutine(_SpinningAttackDuration));
    }

    void Jump()
    {
        _isCheckingPhat = false;
        StartCoroutine(FollowPlayerWithStopsRoutine(_jumpDelay, _jumpCount, _jumpSpeed));
    }

    void BeeShield()
    {
        if(GameObject.FindGameObjectWithTag("BeeShield"))
        {   
            CountAttacks();
            return;
        }
        _currentProyectils.Clear();
        _currentProyectils.Add(_beeShield);
        OnInvokeBeeShield?.Invoke();
        StartCoroutine(WaitUnitlFinishAnimationRoutine("throwUpBeeVomit", true));
    }

    void HorizontalVerticalBees()
    {   
        //WaitUnitlFinishAnimationRoutine("throwUpBeeVomit", false);
        
        StartCoroutine(SidesProyectilsRoutine());
    }

    public void FinishSwarm()
    {
        _currentSpinningSwarm.GetComponent<SpinningSwarm>().FinishSwarm();
    }

    IEnumerator SidesProyectilsRoutine()
    {
        List<Proyectil> VProyectils = new List<Proyectil>();
        List<Proyectil> HProyectils = new List<Proyectil>();
        int i = 0;
        foreach (Transform point in _vertical.transform)
        {
            VProyectils.Add(Instantiate(_swarmProyectil, point.position, Quaternion.identity).GetComponent<Proyectil>());
            VProyectils[i]._direction = Vector3.right;
            VProyectils[i].customDirection = true;
            yield return new WaitForSeconds(.5f);
            i++;
        }
        i = 0;
        foreach (Transform point in _vertical.transform)
        {
            VProyectils[i].StartShoot();
            i++;
            yield return new WaitForSeconds(.5f);
        }
        
        yield return new WaitForSeconds(5);

        i = 0;
        foreach (Transform point in _horizontal.transform)
        {
            HProyectils.Add(Instantiate(_swarmProyectil, point.position, Quaternion.identity).GetComponent<Proyectil>());
            HProyectils[i]._direction = Vector3.up;
            HProyectils[i].customDirection = true;
            yield return new WaitForSeconds(.5f);
            i++;
        }
        i = 0;
        foreach (Transform point in _horizontal.transform)
        {
            HProyectils[i].StartShoot();
            i++;
            yield return new WaitForSeconds(.5f);
        }
        
        CountAttacks();
    }
}

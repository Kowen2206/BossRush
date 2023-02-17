using System;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;


//Todo: 
//Crear un script llamado SlimeBuble que se tendrá la logica para instanciar una mini copia del boss o hacer daño al player y destruirse según sea el caso.
//Crear un script Para el controlar el congelamiento del boss.
[RequireComponent(typeof(NavMeshAgent), typeof(Proyectil))]
public class SecondBoss : MonoBehaviour
{
    NavMeshAgent _agent;
    Proyectil _bossImpulse;
    [SerializeField] int _currentPhase = 0;
    [SerializeField] Transform _restPosition;
    [SerializeField] Attack[] _attacks;
    [SerializeField] GameObject _player, _slimeBullets;
    [SerializeField] float _followSpeed = 7, _followTime;
    TrowSlimeBubles _ThrowSlimeBubles;
    AbsorbItems _absorbItmes;
    Animator _animator;
    bool _followingPlayer = true;
    protected delegate void OnPlaceAction();
    protected OnPlaceAction onGetInRestPlace;
    string _lastAttack;
    Vector3 _lastBossPosition;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _ThrowSlimeBubles = GetComponent<TrowSlimeBubles>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Start()
    {
        FollowPlayer();
    }

    void Update()
    {
        if(transform.position == _lastBossPosition)
            _animator.SetBool("Walking", false);
        else
        { 
            if(_agent.enabled && _followingPlayer)
            {
                _agent.SetDestination(_player.transform.position);
                _animator.SetBool("Walking", true);
            }
        }
        _lastBossPosition = transform.position;
    }

    public void FollowPlayer()
    {
        _followingPlayer = true;
        _agent.enabled = true;
        _agent.speed = _followSpeed;
        _agent.SetDestination(_player.transform.position);
        StartCoroutine(FollowPlayerRoutine());
    }

    public void RandomAttack()
    {
        Debug.Log("attack");
        int RandomIndex = UnityEngine.Random.Range(0, _attacks.Length);
        string attack = _attacks[RandomIndex].name;
        while(attack == _lastAttack) { attack = _attacks[RandomIndex].name; }
        Debug.Log(attack);
        switch (attack)
        {
            case "Punch":
                Punch();
                break;
            case "SlimeBullets":
                StartCoroutine(ShootSlimeBulletsRoutine());
                break;
            case "Copies":
                StartCoroutine(ShootSlimeBublesRoutine());
                break;
            case "Absorb":
                StartCoroutine(AbsorbItmesRoutine());
                break;
            default:
                break;
        }
    }

    public void Punch()
    {
        _followingPlayer = false;
        _agent.enabled = false;
        _animator.SetTrigger("Punch");
    }

    IEnumerator ShootSlimeBublesRoutine()
    {
        if(GameObject.FindGameObjectsWithTag("BossCopy").Length < 3)
        {
            _followingPlayer = false;
            _agent.SetDestination(_restPosition.position);
            yield return new WaitUntil(() => _agent.remainingDistance - _agent.stoppingDistance < 0.1f && !_agent.pathPending);
            _ThrowSlimeBubles._throwBubles = true;
        }
        else
        RandomAttack();
    }
    
    IEnumerator ShootSlimeBulletsRoutine()
    {
        _followingPlayer = false;
        _agent.SetDestination(_restPosition.position);
        yield return new WaitUntil(() => _agent.remainingDistance - _agent.stoppingDistance < 0.1f && !_agent.pathPending);
        _animator.SetTrigger("Bullet");
        yield return new WaitForSeconds(GetAnimationLength("ThrowAcid"));
        FollowPlayer();
    }

    IEnumerator AbsorbItmesRoutine()
    {
        _followingPlayer = false;
        _agent.SetDestination(_restPosition.position);
        yield return new WaitUntil(() => _agent.remainingDistance - _agent.stoppingDistance < 0.1f && !_agent.pathPending);
        _absorbItmes.IsLookingForItems = true;
    }

    public void InstantiateSlimeBullet()
    {
        Instantiate(_slimeBullets, transform.position, Quaternion.identity);
    }

    public float GetAnimationLength(string animName){
        float duration = 0;
        AnimationClip[] animations = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animation in animations)
        {
            if(animation.name == animName){
                duration = animation.length;
            }
            
        }
        return duration;
    }

    IEnumerator FollowPlayerRoutine()
    {
       // _animator.SetBool("Walking", true);
        yield return new WaitForSeconds(_followTime);
        RandomAttack();
    }

    [Serializable]
    public struct Attack
    {
        public string name;
        public int phase;
        public int probaility;
    }

}

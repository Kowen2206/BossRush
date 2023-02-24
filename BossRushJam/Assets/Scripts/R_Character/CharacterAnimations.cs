using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    Animator _animator;
    [Header("Parameters")]
    [SerializeField] string _running, _attacking;
    [Header("Animation Names")]
    [SerializeField] string _attack, _flamethrowerAttack, _acidAttack, _chainSawAttack, _scalpelAttack;
    string _currentAnim, defaultAnim;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentAnim = defaultAnim = "idle";
    }

    public void Run(bool isRunning)
    {
        if(isRunning)
        _currentAnim = _running;
        _animator.SetBool(_running, isRunning);
    }

    public void Attack()
    {
        if(_currentAnim != _attack)
        {
            _currentAnim = _attack;
            _animator.SetBool(_attacking, true);
            _animator.Play(_attack);
        }
        
    }

    public void FlamethrowerAttack()
    {   if(_currentAnim != _flamethrowerAttack)
        {
            _currentAnim = _flamethrowerAttack;
            _animator.SetBool(_attacking, true);
            _animator.Play(_flamethrowerAttack);
        }
        
    }

    public void AcidAttack()
    {   if(_currentAnim != _acidAttack)
        {
            _currentAnim = _acidAttack;
            _animator.SetBool(_attacking, true);
            _animator.Play(_acidAttack);
        }
        
    }

    public void ChainSawAttack()
    {   if(_currentAnim != _chainSawAttack)
        {
            _currentAnim = _chainSawAttack;
            _animator.SetBool(_attacking, true);
            _animator.Play(_chainSawAttack);
        }
        
    }

    public void ScalpelAttack()
    {   if(_currentAnim != _scalpelAttack)
        {
            _currentAnim = _scalpelAttack;
            _animator.SetBool(_attacking, true);
            _animator.Play(_scalpelAttack);
        }
        
    }
    public void FinishAttack()
    {
        
        _currentAnim = defaultAnim;
        _animator.SetBool(_attacking, false);
    }
}

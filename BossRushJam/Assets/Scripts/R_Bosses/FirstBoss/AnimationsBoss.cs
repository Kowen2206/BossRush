using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsBoss : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string Walking, Tackling, Running, Spinning, ThrowingRock, Jumpping, TrowUpBees, TrowClaws, Recover;
    public void Walk(bool isWalking) => _animator.SetBool(Walking, isWalking);
    public void Run(bool isRunning) => _animator.SetBool(Running, isRunning);
    public void Spin(bool isSpinning) => _animator.SetBool(Spinning, isSpinning);
    public void ThrowRocks(bool isThrowingRocks) => _animator.SetBool(ThrowingRock, isThrowingRocks);
    //StartTackle
    public void Tackle(bool isTackling) => _animator.SetBool(Tackling, isTackling);
    public void Jump(bool isJumpping) => _animator.SetBool(Jumpping, isJumpping);
    public void TrowUpSwarmBees() => _animator.SetTrigger(TrowUpBees);
    public void TrowingClaws() => _animator.SetTrigger(TrowClaws);

    public void SeriousDamageAnim()
    {
        RecoverForDamage(false);
        _animator.Play("StartRecover");
    }

    public void RecoverForDamage(bool value)
    {
        _animator.SetBool(Recover, value);
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
    void Update()
    {
        
    }
}

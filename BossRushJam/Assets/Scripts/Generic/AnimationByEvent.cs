using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimationByEvent : MonoBehaviour
{
    Animator _animator;
    [SerializeField] string _animationParameterName;
    [SerializeField] AnimationType _animationType;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void playBooleanAnimation(bool value) =>_animator.SetBool(_animationParameterName, value);
    public void playFloatAnimation(float value) =>_animator.SetFloat(_animationParameterName, value);
    public void playTriggerAnimation() =>_animator.SetTrigger(_animationParameterName);
    
    public enum AnimationType
    {
        Trigger,
        Boolean, 
        Float
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsBoss : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string Walking, Tackle;
    void Walk(bool isWalking) => _animator.SetBool(Walking, isWalking);
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

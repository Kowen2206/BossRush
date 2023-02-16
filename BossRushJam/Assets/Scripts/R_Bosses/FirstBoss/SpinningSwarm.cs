using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpinningSwarm : MonoBehaviour
{
    Animator _animator;
    [SerializeField] string _finishSwarmAnim;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishSwarm()
    {
        _animator.SetBool(_finishSwarmAnim, true);
    }

    public void DestroySwarm()
    {
        Destroy(gameObject);
    }
}

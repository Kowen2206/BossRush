using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 0.1f;
    [SerializeField] UnityEvent OnDestroy;
    [SerializeField] private bool _autoDestroy = false;

    void Start()
    {
        if(_autoDestroy) DestroyWithDelay();
    }

    public void DestroyWithDelay(float delay = 0){
        if(delay != 0) _destroyDelay = delay;
         Destroy(gameObject, _destroyDelay);
         OnDestroy?.Invoke();
    }

}

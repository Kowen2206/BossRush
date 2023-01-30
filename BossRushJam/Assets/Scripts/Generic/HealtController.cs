using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class HealtController : MonoBehaviour
{

    [SerializeField] private float _currentHealt, _minHealt, _initialHealt = 1;
     float _healtPercentage;
    [SerializeField] UnityEvent OnDie;
    [SerializeField] UnityEvent<float> OnRecoverHealt, OnReciveDamage;
    public static bool canReciveDamage = true;

    void Start()
    {
        _minHealt = 0;
        _currentHealt = _initialHealt;
    }

    void calculateHealtPercent()
    {
        _healtPercentage = (_currentHealt * 100) / _initialHealt;
    }


   public void decreaseHealt(float damage)
   {
    if(!canReciveDamage) return;
        _currentHealt -= damage;
            calculateHealtPercent();
            OnReciveDamage.Invoke(_healtPercentage);
            if(_currentHealt < _minHealt)
            {
                _currentHealt = _minHealt;
                OnDie?.Invoke();
            }
   }

   public void IncreaseHealt(float helat)
   {
        _currentHealt += helat;
            calculateHealtPercent();
            OnRecoverHealt?.Invoke(_healtPercentage);
            if(_currentHealt > _initialHealt)
            {
                _currentHealt = _initialHealt;
            }
            calculateHealtPercent();
   }
}

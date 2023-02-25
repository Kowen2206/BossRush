using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class HealtController : MonoBehaviour
{

    [SerializeField] private float _currentHealt, _minHealt = 0, _initialHealt = 100;
     float _healtPercentage;
    [SerializeField] UnityEvent OnDie;
    [SerializeField] UnityEvent OnRecoverHealt, OnReciveDamage;
    [SerializeField] UnityEvent<float> OnChangeHealtPercentage;
    public bool canReciveDamage = true;

    public float CurrentHealt
    {
        get => _currentHealt;
    }
    
    public bool CanReciveDamage
    {
        get => canReciveDamage;
        set => canReciveDamage = value;
    }
    void Start()
    {
        _currentHealt = _initialHealt;
        OnChangeHealtPercentage?.Invoke(GetHealtPercentage());
    }

   public void decreaseHealt(float damage)
   {
    if(!canReciveDamage) return;
        _currentHealt -= damage;
        OnReciveDamage.Invoke();
        if(_currentHealt < _minHealt)
        {
            _currentHealt = _minHealt;
            OnDie?.Invoke();
        }
        OnChangeHealtPercentage?.Invoke(GetHealtPercentage());
   }

   public void IncreaseHealt(float helat)
   {
        _currentHealt += helat;
        OnRecoverHealt?.Invoke();
        if(_currentHealt > _initialHealt)
        {
            _currentHealt = _initialHealt;
        }
        OnChangeHealtPercentage?.Invoke(GetHealtPercentage());
   }

   public float GetHealtPercentage()
   {
        
        _healtPercentage = _currentHealt * 1/ _initialHealt;
        return _healtPercentage;
   }
}

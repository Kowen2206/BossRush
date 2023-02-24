using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeponDurability : MonoBehaviour
{
    public float durability, _decrementPercentage;
    public bool _decreaseBySecond;
    [SerializeField] UnityEvent OnWeaponBreaks;
    
    public void DrecreaseDurability()
    {
        durability -= _decrementPercentage;
        if(durability <= 0)
        OnWeaponBreaks?.Invoke();
    }
}

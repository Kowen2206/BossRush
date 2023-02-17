using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbsorbItems : MonoBehaviour
{
    [SerializeField] string _targetItemId;
    [SerializeField] int _targetItemsCount;
    [SerializeField] float _absortionSpeed;
    int _currentItemsCount;
    [SerializeField] bool _isLookingForItems;
    [SerializeField] UnityEvent OnFinishAbsorb;
    
    public bool IsLookingForItems
    {
        get => _isLookingForItems;
        set => _isLookingForItems = value;
    }
    // Start is called before the first frame update
    void StartAbsorbtion()
    {
        _isLookingForItems = true;
    }

    void Update()
    {
        if(_isLookingForItems)
        transform.localScale += Vector3.one * 2 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_targetItemId) && _isLookingForItems && _currentItemsCount < _targetItemsCount)
        {
            Proyectil proyectil = other.gameObject.AddComponent<Proyectil>();
            proyectil.Stop();
            proyectil.TargetId = transform.parent.tag;
            proyectil.ProyectilSpeed = _absortionSpeed;
            proyectil.StopInTargetPosition = true;
            proyectil.StartShoot();
            if(_currentItemsCount == _targetItemsCount)
            {
                _isLookingForItems = false;
                OnFinishAbsorb?.Invoke();
            }
            _currentItemsCount++;
        }
    }
}

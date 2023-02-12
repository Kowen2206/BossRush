using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] float _proyectilDelay;
    [SerializeField] GameObject _proyectil;
    HealtController _shieldHealt;
    float _timer = 0;
    
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Boss").transform);
        _shieldHealt = GetComponent<HealtController>();
    }
    
    void Update()
    {
        if(_timer >= _proyectilDelay)
        {
          Proyectil proyectil =  Instantiate(_proyectil, transform.position, Quaternion.identity).GetComponent<Proyectil>();
          proyectil.DrawTrayectoryLine = false;
          proyectil.customDirection = false;
          proyectil.StartShoot();
            _timer = 0;   
        }
        _timer += Time.deltaTime;
    }
}

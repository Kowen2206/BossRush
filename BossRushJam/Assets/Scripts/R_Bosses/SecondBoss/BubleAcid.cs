using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubleAcid : MonoBehaviour
{
    [SerializeField] GameObject _miniBossPrefab;
    [SerializeField] int _damage;
    
    void CreateMiniBoss()
    {
      GameObject miniBoss =  Instantiate(_miniBossPrefab, transform.position, Quaternion.identity);
      miniBoss.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HealtController>().decreaseHealt(_damage);
        }
    }
}

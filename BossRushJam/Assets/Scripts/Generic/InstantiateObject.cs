using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField] GameObject _object;
    // Start is called before the first frame update
    public void Instantiate()
    {
        Instantiate(_object, transform.position, Quaternion.identity);
    }
}

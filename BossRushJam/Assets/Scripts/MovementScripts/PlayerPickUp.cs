using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private Transform _grabPoint;

    private GameObject _grabbedObject;
    private int layerIndex;
    private bool _canPickUp=true;


    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canPickUp == true && collision.tag == "Object" && Input.GetKeyDown(KeyCode.F))
        {
            _canPickUp = false;
            _grabbedObject = collision.gameObject;
            _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
            _grabbedObject.transform.position = _grabPoint.position;
            _grabbedObject.transform.SetParent(transform);

        }
    }

    private void Update()
    {
        if (_canPickUp == false && Input.GetKey(KeyCode.F)&&Input.GetKey(KeyCode.Mouse0))
        {
            _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            _grabbedObject.transform.SetParent(null);
            _grabbedObject = null;
            _canPickUp = true;
        }
    }

}

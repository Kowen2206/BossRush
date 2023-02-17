using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private Transform _grabPoint;

    private AnimationManager anim;
    public GameObject ft;

    private void Start()
    {
        anim = FindObjectOfType<AnimationManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mechero" && Input.GetKeyDown(KeyCode.Mouse1))
        {
           
            anim.tipoAtaque = 1;
            ft.SetActive(true);
            Destroy(collision);
        }

        if (collision.tag == "Acido" && Input.GetKeyDown(KeyCode.Mouse1))
        {

            anim.tipoAtaque = 2;
            Destroy(collision);
        }
    }

}

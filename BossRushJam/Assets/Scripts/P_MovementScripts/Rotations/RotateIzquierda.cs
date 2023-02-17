using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIzquierda : MonoBehaviour
{
    public GameObject armas;
    private AnimationManager anim;
    private void Start()
    {
        anim = FindObjectOfType<AnimationManager>();
    }
    private void OnMouseEnter()
    {
        anim.estado = 0;
        armas.transform.Rotate(0, 0, -180);
    }

    private void OnMouseExit()
    {
        armas.transform.Rotate(0, 0, 0);
    }

}

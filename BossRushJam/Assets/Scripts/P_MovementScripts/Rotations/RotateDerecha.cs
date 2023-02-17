using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDerecha : MonoBehaviour
{
    public GameObject armas;
    private AnimationManager anim;
    private void Start()
    {
        anim = FindObjectOfType<AnimationManager>();
    }
    private void OnMouseEnter()
    {
        anim.estado = 1;
        //armas.transform.Rotate(0, 0, 0);
    }
}

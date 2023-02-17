using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEspalda : MonoBehaviour
{
    private AnimationManager anim;
    private void Start()
    {
        anim = FindObjectOfType<AnimationManager>();
    }
    private void OnMouseEnter()
    {
        anim.estado = 2;
    }
}

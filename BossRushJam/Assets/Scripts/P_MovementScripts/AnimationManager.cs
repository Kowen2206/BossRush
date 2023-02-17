using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private PlayerMovement playerMovement;
    public int estado;
    private string currentState;
    public int tipoAtaque=0;

    const string PLAYER_IDLE = "iddleDerecha";
    const string PLAYER_IZQUIERDA = "correrIzquierdaAnim";
    const string PLAYER_DERECHA = "correrDerechaAnim";
    const string PLAYER_ESPALDAS = "correrEspaldasAnim";
    const string PLAYER_FRENTE = "correrFrenteAnim";

    const string MECHERO_IDDLE = "mecheroIddle";
    const string MECHERO_DERECHA = "mecheroDerecha";
    const string MECHERO_IZQUIERDA = "mecheroIzquierda";

    const string ACIDO_DERECHA = "acidoDerecha";
    const string ACIDO_IZQUIERDA = "acidoIzquierda";

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(tipoAtaque == 0 && playerMovement.isWalking==true)
        {
            switch(estado)
            {
                case 0:
                    ChangeAnimationState(PLAYER_IZQUIERDA);
                    break;
                case 1:
                    ChangeAnimationState(PLAYER_DERECHA);
                    break;
                case 2:
                    ChangeAnimationState(PLAYER_ESPALDAS);
                    break;
                case 3:
                    ChangeAnimationState(PLAYER_FRENTE);
                    break;

            }
        }

        if(tipoAtaque == 1 && playerMovement.isWalking == true)
        {
            switch(estado)
            {
                case 0:
                    ChangeAnimationState(MECHERO_IZQUIERDA);
                    break;
                case 1:
                    ChangeAnimationState(MECHERO_DERECHA);
                    break;
            }
        }

        if (tipoAtaque == 2 && playerMovement.isWalking == true)
        {
            switch (estado)
            {
                case 0:
                    ChangeAnimationState(ACIDO_IZQUIERDA);
                    break;
                case 1:
                    ChangeAnimationState(ACIDO_DERECHA);
                    break;
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   //Metodo publico para el boton PlayGame
   public void Play()
   {
    Debug.Log("Iniciando juego");
    SceneManager.LoadScene(1);

   }
   
}

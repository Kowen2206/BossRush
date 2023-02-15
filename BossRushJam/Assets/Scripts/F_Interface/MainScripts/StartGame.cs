using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StartGame : MonoBehaviour
{
   //Metodo publico para el boton PlayGame
   public void Play()
   {
    Debug.Log("Iniciando juego");
    StartCoroutine(StartingGame()); 
    SceneManager.LoadScene(1);
    
   }
   IEnumerator StartingGame()
   {
     yield return new WaitForSeconds(1.0f);
     SceneManager.LoadScene(2);
   }


   
}

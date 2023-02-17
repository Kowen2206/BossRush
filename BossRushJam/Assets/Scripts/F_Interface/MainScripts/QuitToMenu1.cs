using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitToMenu1 : MonoBehaviour
{
    // Start is called before the first frame update
   public void Play1()
   {
    Debug.Log("Iniciando juego");
    SceneManager.LoadScene(0);
   }
}

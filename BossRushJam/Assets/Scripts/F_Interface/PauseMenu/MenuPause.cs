using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
//pausar el juego
{
  [SerializeField] private GameObject Pause;
  [SerializeField] private GameObject PauseMenu;
    
    public void pause()
    {
        Time.timeScale = 0f;
        Pause.SetActive (false);
        PauseMenu.SetActive (true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
         Pause.SetActive (true);
        PauseMenu.SetActive (false);
    }

}

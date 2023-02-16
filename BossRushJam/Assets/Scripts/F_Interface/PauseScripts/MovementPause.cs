using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class MovementPause : MonoBehaviour
{ 
    [Header("Pause")]
    public GameObject PauseMen;
    public GameObject Settings1;
    public GameObject Origins;
    public AudioSource Fuente;
    public AudioClip click;
    void start()
    {
        Fuente.clip = click;
        StartCoroutine(PauseMenus());
    }
    public void Resumen()
    {
        Origins.SetActive(false);
    }
    public void Settings2()
    {
        Origins.SetActive(true);
        Settings1.SetActive(false);
    }
    public void settingsPause()
    {
        PauseMen.SetActive(false);
        Settings1.SetActive(true);
    }
    public void Exit1()
    {
        PauseMen.SetActive(true);
        Settings1.SetActive(false);

    }
    public void sonido()
    {
        Fuente.Play();
    }
     IEnumerator PauseMenus()
    {
        yield return new WaitForSeconds(0f);
        Origins.SetActive(false);
        Settings1.SetActive(false);
    }
}

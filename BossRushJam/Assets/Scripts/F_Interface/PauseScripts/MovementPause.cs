using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MovementPause : MonoBehaviour
{ [Header("Pause")]
    public GameObject PauseMen;
    public GameObject Settings1;
    public AudioSource fuentes;
    public AudioClip Clicks;
    void start()
    {
        StartCoroutine(PauseMenu());
        fuentes.clip = Clicks;

    }
    void update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseMen.SetActive(true);
            Debug.Log ("ESC");
        }
    }
    
    IEnumerator PauseMenu()
    {
        yield return new WaitForSeconds(0.0f);
        PauseMen.SetActive(false);
        Settings1.SetActive(false);
    }
    public void Resumen()
    {
        PauseMen.SetActive(false);
        Settings1.SetActive(false);
    }
    public void settingsPause()
    {
        PauseMen.SetActive(false);
        Settings1.SetActive(true);
    }
    public void Exit()
    {
        PauseMen.SetActive(true);
        Settings1.SetActive(false);

    }
    public void Sonido0()
    {
        fuentes.Play();
    }
    public void Scene0()
    {
        SceneManager.LoadScene(0);
    }
    
    
}

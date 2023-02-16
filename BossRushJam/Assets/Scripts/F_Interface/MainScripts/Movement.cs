using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
// Variables para el movimiento de la interfaz
    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject KeyBoardImage;
    public GameObject Fade;
    public AudioSource fuente;
    public AudioClip Click;
    void Start()
    {
        StartCoroutine(Inicial());
        fuente.clip = Click;
    }
        
    
    public void mainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
    }
     public void settingsMenu()
    {
        SettingsMenu.SetActive(true);
        MainMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
    }
     public void keyBoardImage()
    {
        KeyBoardImage.SetActive(true);
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(false);
    }
public void Quit()
{
    Application.Quit();
}
IEnumerator Inicial()
{
    yield return new WaitForSeconds(0.0f);
    MainMenu.SetActive(true);
    SettingsMenu.SetActive(false);
    KeyBoardImage.SetActive(false);

}
public void Sonido()
{
    fuente.Play();
}
}

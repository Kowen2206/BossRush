using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuToSettings : MonoBehaviour
{
    public GameObject[] ControlMusica;
    public GameObject[] ControlFX;

// Variables para el movimiento de la interfaz
    [Header("options")]
    public Slider VolumeMusic;
    public Slider VolumeFX;
    public Toggle Mute;

    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject KeyBoardImage;

    private void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
        //Control de Audio
        ControlMusica = GameObject.FindGameObjectsWithTag("Musicas");
        VolumeMusic.value = PlayerPrefs.GetFloat("MusicSave", 1f);
     }
     private void update()
     {
        foreach (GameObject au in ControlMusica)
        au.GetComponent<AudioSource>().volume = VolumeMusic.value;
     }
     public void GuardarVolumen()
     {
        PlayerPrefs.SetFloat("MusicSave", VolumeMusic.value);
     }

     private void OpenPanel(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
    
        Menu.SetActive(true);
    }
   
 
}

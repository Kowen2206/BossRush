using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuToSettings : MonoBehaviour
{
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
     }
    

     private void OpenPanel(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
    
        Menu.SetActive(true);
    }
   
 
}

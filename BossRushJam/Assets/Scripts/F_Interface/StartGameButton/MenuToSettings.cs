using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToSettings : MonoBehaviour
{
    private string [] TextOptions = new string[3];
// Variables para el movimiento de la interfaz
    [Header("options")]
    public Slider VolumeFX;
    public Slider VolumeMusic;
    public Toggle Mute;

    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMen;
    public GameObject KeyBoardImage;
    
    void Start()
    {
        MainMenu.SetActive(true);
        SettingsMen.SetActive(false);
        KeyBoardImage.SetActive(false);
       

    //Textos a lo random
    TextOptions[0] = "Made in Mexico";
    TextOptions[1] = "¡Lucha lucha!";
    TextOptions[2] = "¡Rompe las cajas!";

    


        
     }

     public void OpenPanel(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMen.SetActive(false);
        KeyBoardImage.SetActive(false);
    
        Menu.SetActive(true);
    }
   
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuToSettings : MonoBehaviour
{
   

    [Header("options")]
    public Slider VolumeFX;
    public Slider VolumeMusic;
    public Toggle Mute;

    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
     

    public void OpenPanel(GameObject Menu)
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(false);
        
        

        Menu.SetActive(true);
    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;
public class MenuToSettings : MonoBehaviour
{
    private string [] TextOptions = new string[3];
    [SerializeField] private ParticleSystem Acid;
 
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

        
        Acid.Play();


       

    //Textos a lo random
    TextOptions[0] = "Made in Mexico";
    TextOptions[1] = "¡Lucha lucha!";
    TextOptions[2] = "¡Rompe las cajas!";

    


        
     }

     private void OpenPanel(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
    
        Menu.SetActive(true);
    }
   
 
}

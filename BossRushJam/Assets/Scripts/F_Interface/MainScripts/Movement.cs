using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
// Variables para el movimiento de la interfaz
    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject KeyBoardImage;
    public GameObject Fade;
        
    
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
    




}

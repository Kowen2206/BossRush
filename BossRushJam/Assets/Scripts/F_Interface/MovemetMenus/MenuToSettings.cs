using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuToSettings : MonoBehaviour
{
    public Animator animator;
    public float SliderValue;
    public Image ImagenMute;
// Variables para el movimiento de la interfaz
    [Header("options")]
    public Slider VolumeMusic;
    public Slider VolumeFX;
    public Toggle Mute;

    [Header("menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject KeyBoardImage;
    public GameObject Fade;

    private void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
        //Control de Audio
        VolumeMusic.value = PlayerPrefs.GetFloat("Volumenaudio", 1f);
        AudioListener.volume = VolumeMusic.value;
        ImagenMute.enabled = false;
        NomuteSearch();

     }
     private void ChangeSlider(float valor)
     {
        SliderValue = valor;
        PlayerPrefs.SetFloat("Volumenaudio",SliderValue);
        AudioListener.volume = VolumeMusic.value;
        NomuteSearch();
     }

     private void NomuteSearch()
     {
        if (SliderValue == 0)
        {
            ImagenMute.enabled = true;
        }
        else
        {
            ImagenMute.enabled = false;
        }

     }
    

     private void OpenPanel(GameObject Menu)
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        KeyBoardImage.SetActive(false);
        FadeOut();
        Menu.SetActive(true);
    }

    private void FadeOut()
    {
        animator.Play("FadeOut");

    }
   
 
}

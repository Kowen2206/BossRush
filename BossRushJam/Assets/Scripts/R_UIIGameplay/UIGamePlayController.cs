using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIGamePlayController : MonoBehaviour
{
    public static UIGamePlayController instance;
    [SerializeField] TextMeshProUGUI _smallMessageText;
    [SerializeField] GameObject _smallMessageBackground;
    [SerializeField] Image _healtBar, _dashBar, _selectedItem;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _smallMessageText.text = "";
    }
    
    public void ShowSmallMessage(string message)
    {
        _smallMessageBackground.SetActive(true);
        _smallMessageText.text = message;
    }
    public void HideSmallMessage()
    {
        _smallMessageBackground.SetActive(false);
        _smallMessageText.text = "";
    }


    public void UpdateHealtBar(float percentage)
    {
        ModifyStatusBar(_healtBar, percentage);
    }
    public void UpdateDashBar(float percentage)
    {
        
        ModifyStatusBar(_dashBar, percentage);
    }

    public void ModifyStatusBar(Image bar, float percentage)
    {
        bar.fillAmount = percentage;
    }

    public void SetSelectedItem(WeaponsList weapon)
    {
      _selectedItem.sprite = WeaponController.instance.GetWeaponImage(weapon);
    }
}


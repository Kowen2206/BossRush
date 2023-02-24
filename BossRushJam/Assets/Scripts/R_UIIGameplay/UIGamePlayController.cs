using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIGamePlayController : MonoBehaviour
{
    public static UIGamePlayController instance;
    [SerializeField] TextMeshProUGUI _smallMessageText;
    [SerializeField] Image _HealtBar;
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
        _smallMessageText.text = message;
    }
    public void HideSmallMessage()
    {
        _smallMessageText.text = "";
    }
}

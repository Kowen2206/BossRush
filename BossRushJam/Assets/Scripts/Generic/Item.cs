using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    [SerializeField] string _itemMessage = "No message";
    public bool triggerActive;

    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && triggerActive)
        {
            UIGamePlayController.instance.ShowSmallMessage(_itemMessage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && triggerActive)
        {
            UIGamePlayController.instance.HideSmallMessage();
        }
    }
}

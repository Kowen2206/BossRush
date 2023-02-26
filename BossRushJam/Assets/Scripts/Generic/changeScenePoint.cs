using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenePoint : MonoBehaviour
{
    [SerializeField] string _message = "press F to enter to the elevator", _scene = "FirstBoss";
    bool _changeScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F)){ _changeScene = true;}
        if(Input.GetKeyUp(KeyCode.F)) _changeScene = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            UIGamePlayController.instance.ShowSmallMessage(_message);
            if(_changeScene) SceneManager.LoadScene(_scene);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            UIGamePlayController.instance.HideSmallMessage();
        }
    }
}

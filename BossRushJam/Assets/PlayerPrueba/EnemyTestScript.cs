using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    private int i=0;
    public int life = 100;

    void Update()
    {
        if(life<=0)
        {
            Debug.Log("*se muere en idioma minion waaasdgafdsvdsa*");
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        i += 1;
        Debug.Log("Hit " + i + ": " + life);
    }
}

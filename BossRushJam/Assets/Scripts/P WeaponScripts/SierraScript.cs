using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SierraScript : MonoBehaviour
{
    public int sierraDamage = 20;
    public float lastCutTime;
    public float sierraCooldown = .5f;
    private EnemyTestScript enemy;
    private bool _canAttack = false;
    private Renderer rend;
    public int firstStrike =30;
    private bool canFirstStrike = true;

    private void Start()
    {
        enemy = FindObjectOfType<EnemyTestScript>();
        rend = gameObject.GetComponent<Renderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _canAttack = true;
            rend.material.color = Color.red;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _canAttack = false;
            rend.material.color = Color.black;
            canFirstStrike = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canAttack == true)
        {
            if (Time.time - lastCutTime < sierraCooldown)
            {
                return;
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                if(canFirstStrike==true)
                {
                    enemy.life -= firstStrike;
                    canFirstStrike = false;
                }
                enemy.life -= 20;
                enemy.Damage();
                lastCutTime = Time.time;
            }
        }
    }
}

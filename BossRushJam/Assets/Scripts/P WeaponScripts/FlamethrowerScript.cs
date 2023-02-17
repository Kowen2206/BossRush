using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerScript : MonoBehaviour
{
    public int burnDamage=20;
    public float lastBurnTime;
    public float attackCooldown=.5f;
    private EnemyTestScript enemy;
    private bool _canAttack=false;
    public Renderer rend;

    private void Start()
    {
        enemy =FindObjectOfType<EnemyTestScript>();
        rend = GetComponent<Renderer>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rend.enabled = true;
            _canAttack = true;
        }
        
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            rend.enabled = false;
            _canAttack = false;
        }    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_canAttack==true)
        {
            if (Time.time - lastBurnTime < attackCooldown)
            {
                return;
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                enemy.Damage();
                lastBurnTime = Time.time;
            }
        }    
    }
}

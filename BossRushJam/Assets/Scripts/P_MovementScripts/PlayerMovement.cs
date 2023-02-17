using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables del Movimiento
    public float movementSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    //variables mouse
    private Vector3 mousePosition;
    private Vector3 direction;
    private float angle;

    //Variables del Dash
    private bool _canDash = true;
    private bool _isDashing;
    public float dashingPower=24f;
    public float dashingCooldown = 1f;
    private float _dashingTime = 0.4f;

    public bool isWalking = false;


    void Update()
    {
        MovementInput(); 

        if (_isDashing)
        {
            return;
        }
        rb.velocity = movement * movementSpeed;
        //PlayerRotation();

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash == true)
        {
            StartCoroutine(Dash());
        }
    }


    private void MovementInput()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movementX, movementY).normalized;

        if(movementX!=0 ||movementY!=0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void PlayerRotation()
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        rb.velocity = dashingPower * new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        _canDash = true;
    }
}

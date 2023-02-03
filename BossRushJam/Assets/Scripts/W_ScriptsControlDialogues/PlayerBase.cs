using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float speed = 3.0f;

    void Start()
    {
        transform.position = new Vector3(-1, 0, -0);

    }


    void Update()
    {     
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * HorizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * VerticalInput);


        //Limite de escenario en el eje X
        if (transform.position.x < -2.5) transform.position = new Vector3((float)-2.5, transform.position.y, transform.position.z);

        if(transform.position.x > 2.7) transform.position = new Vector3((float)2.7, transform.position.y, transform.position.z);


        //limite de escenario en el eje y
        if(transform.position.y < -1) transform.position = new Vector3(transform.position.x, -1, transform.position.z);

        if(transform.position.y > 1) transform.position = new Vector3(transform.position.x, 1, transform.position.z);

    }
}

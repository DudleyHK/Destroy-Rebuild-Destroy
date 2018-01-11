using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float angularSpeed = 0.1f;




    private new Rigidbody rigidbody;






    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }



    private void Update()
    {
        Movement();
    }




    private void Movement()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity += transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.angularVelocity += transform.right * angularSpeed * Time.deltaTime;
        }
    }
}

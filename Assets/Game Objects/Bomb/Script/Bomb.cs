using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Bomb : MonoBehaviour 
{
    private float acceleration = 75f;
    private new Rigidbody rigidbody;




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(transform.position.y < 5f)
            Destroy(this.gameObject);

       
    }

    private void FixedUpdate()
    {
        rigidbody.velocity += Vector3.down * acceleration * Time.fixedDeltaTime;
    }



    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("Bomb Hit: " + other.gameObject.tag);
        var otherObject = other.gameObject;
        if(otherObject.tag == "Building")
        {
            Debug.Log("Bomb Hit: " + otherObject.tag);
            if(otherObject.GetComponent<Renderer>() != null)
            {
                otherObject.GetComponent<Renderer>().material.color = Color.red;
            }
            Destroy(this.gameObject);
        }
    }
}

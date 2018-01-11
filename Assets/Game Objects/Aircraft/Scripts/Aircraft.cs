using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour 
{
    [SerializeField]
    private Rigidbody aircraftRigidbody;






    private void Awake()
    {
        aircraftRigidbody = GetComponent<Rigidbody>();
    }



    private void Update()
    {
        Movement();
    }


    private void Movement()
    {

    }

}

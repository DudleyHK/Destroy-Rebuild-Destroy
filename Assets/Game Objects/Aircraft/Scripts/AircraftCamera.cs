using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftCamera : MonoBehaviour 
{
    [SerializeField]
    private float followSpeed = 5f;
    [SerializeField]
    private GameObject aircraft;


    private void Start()
    {
        aircraft = FindObjectOfType<Aircraft>().gameObject;
        if(!aircraft)
        {
            Debug.Log("ERROR: Cannot find aircraft gameobject");
        }
    }

    private void FixedUpdate()
    {
        var offset = ((transform.forward * -35)  + (transform.up * 15f));
        var position = Vector3.Lerp(transform.position, (aircraft.transform.position + offset), followSpeed * Time.fixedDeltaTime);
        
        //transform.position += ((aircraft.transform.position + offset) - transform.position) * 0.2f;
        var rotation = aircraft.transform.localRotation;

        transform.SetPositionAndRotation(position, rotation);
    }
}

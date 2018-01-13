using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftCamera : MonoBehaviour 
{
    [SerializeField]
    private float followSpeed = 5f;
    [SerializeField]
    private GameObject aircraft;
    [SerializeField]
    private float height = 3f;
    [SerializeField]
    private float rotationDamping = 0.25f;
    [SerializeField]
    private float heightDamping = 0.5f;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float lookAhead = 30f;
    [SerializeField]
    private float bias = 0.96f;


    private void Start()
    {
        aircraft = FindObjectOfType<Aircraft>().gameObject;
        if(!aircraft)
        {
            Debug.Log("ERROR: Cannot find aircraft gameobject");
        }
    }


    private void LateUpdate()
    {
       //Vector3 moveTo = aircraft.transform.position - ((aircraft.transform.forward * distance) + (Vector3.up * height));
       //
       //if(bias >= 1) bias = 1f;
       //
       //// Spring equation.
       //transform.position = moveTo * (1f - bias) + transform.position * bias;
       //
       //
       //transform.LookAt(aircraft.transform.position + (aircraft.transform.forward * lookAhead));
    }



    //private void FixedUpdate()
    //{
    //    //// Calculate the current rotation angles
    //    //var wantedRotationAngle = aircraft.transform.eulerAngles.y;
    //    //var wantedHeight = aircraft.transform.position.y + height;
    //
    //    //var currentRotationAngle = transform.eulerAngles.y;
    //    //var currentHeight = transform.position.y;
    //
    //    //// Damp the rotation around the y-axis
    //    //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
    //
    //    //// Damp the height
    //    //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
    //
    //    //// Convert the angle into a rotation
    //    //var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
    //
    //    //// Set the position of the camera on the x-z plane to:
    //    //// distance meters behind the target
    //    //transform.position = aircraft.transform.position;
    //    //transform.position -= currentRotation * Vector3.forward * distance;
    //
    //    //// Set the height of the camera
    //    //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    //
    //    //// Always look at the target
    //    //transform.LookAt(aircraft.transform);
    //}
}

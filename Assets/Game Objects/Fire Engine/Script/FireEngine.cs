using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: This class would be a fire engine manager ie log the posiitons which players are in, and each role.

// For this the fire engine does everything, later each person will have a different role based on which posiiton they're in.

public class FireEngine : MonoBehaviour 
{

    [SerializeField]
    private float driveSpeed = 150f;
    [SerializeField]
    private float turnSpeed = 75f;
    [SerializeField]
    private float gushSpeed = 100f;
    [SerializeField]
    private GameObject rotationBox;
    [SerializeField]
    private GameObject hose;
    [SerializeField]
    private GameObject waterPrefab;


    private void Start()
    {
        rotationBox = GameObject.Find("Rotation Box - 1");
        if(!rotationBox)
        {
            Debug.Log("ERROR: Rotation box object not found.");
            return;
        }
        
        hose = GameObject.Find("Hose - 1");
        if(!hose)
        {
            Debug.Log("ERROR: Child GameObject (hose) not found");
            return;
        }
    }

    private void OnEnable()
    {
        InputManager.inputDetected += HandleInput;
    }


    private void OnDisable()
    {
        InputManager.inputDetected -= HandleInput;
    }


    private void HandleInput(GameAction gameAction, float value, int ID)
    {
        // if (myID = ID)
        switch(gameAction)
        {
            case GameAction.RT_Axis:
                Drive(value);
                break;
            case GameAction.LT_Axis:
                Reverse(value);
                break;
            case GameAction.LS_X_Axis:
                Turn(value);
                break;
            case GameAction.A_Held:
                CannonOne(value);
                break;
            case GameAction.RS_X_Axis:
                RotateCanonHor(value);
                break;
            case GameAction.RS_Y_Axis:
                RotateCanonVert(value);
                break;
        }
    }


    private void Drive(float value)
    {
        var speed = driveSpeed * value;
        transform.position -= transform.forward * speed * Time.deltaTime;
    }

    private void Reverse(float value)
    {
        var speed = driveSpeed * -value;
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void Turn(float value)
    {
        var speed = value * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, speed, 0f);
    }


    private void CannonOne(float value)
    {
        var spawnPoint = hose.transform.position + (transform.forward * 10f);

        var clone = Instantiate(waterPrefab, spawnPoint, Quaternion.identity);
        var clone2 = Instantiate(waterPrefab, spawnPoint, Quaternion.identity);

        clone.GetComponent<Rigidbody>().velocity += transform.forward * gushSpeed;
        clone2.GetComponent<Rigidbody>().velocity += transform.forward * gushSpeed / 2f;
    }



    private void RotateCanonHor(float value)
    {
        if(value != 0)
            Debug.Log("Horizontal rotation of canon currently being shot with");
    }


    private void RotateCanonVert(float value)
    {
        if(value != 0)
            Debug.Log("Vertical rotation of canon currently being shot with");
    }

}

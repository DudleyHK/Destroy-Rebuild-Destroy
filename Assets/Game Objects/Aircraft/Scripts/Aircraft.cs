using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float acceleration = 50f;
    [SerializeField]
    private float yawSpeed = 4.5f;
    [SerializeField]
    private float angularSpeed = 0.1f;
    [SerializeField]
    private int ID = 1;
    [SerializeField]
    private Camera aircraftCamera;
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private BombCamera bombCamera;
    [SerializeField]
    private GameObject lastBomb = null;
   

    
    private new Rigidbody rigidbody;






    private void Awake()
    {
        rigidbody  = GetComponent<Rigidbody>();
        bombCamera = GetComponentInChildren<BombCamera>();
    }


    private void OnEnable()
    {
        InputManager.InputDetected += HandleInput;
    }


    private void OnDisable()
    {
        InputManager.InputDetected -= HandleInput;
    }


    private void Update()
    {
        
    }

    private void HandleInput(GameAction gameAction, float value, int ID)
    {
        // if (myID == ID)
        // {

        switch(gameAction)
        {
            case GameAction.LS_X_Axis:
                Roll(value);
                break;
            case GameAction.LS_Y_Axis:
                Pitch(value);
                break;
            case GameAction.RT_Axis:
                Forward(value);
                break;
            case GameAction.A_Down:
                DropBomb(value);
                break;
            case GameAction.B_Down:
                BombCamera(value);
                break;
            case GameAction.LB_Held:
                Yaw(value);
                break;
            case GameAction.RB_Held:
                Yaw(value);
                break;
        }

        // }
    }


    private void Pitch(float value)
    {
        rigidbody.angularVelocity += transform.right * (-value * angularSpeed) * Time.deltaTime;
        //transform.Rotate(value, 0f, 0f, Space.Self);
    }

    private void Yaw(float value)
    {
        rigidbody.angularVelocity += transform.up * (value * angularSpeed) * Time.deltaTime;
       // transform.Rotate(0f, value * yawSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void Roll(float value)
    {
        rigidbody.angularVelocity += transform.forward * (-value * angularSpeed) * Time.deltaTime;
        //transform.Rotate(0f, 0f, -value, Space.Self);
    }


    private void Forward(float value)
    {
       var forwardSpeed = value * speed;
       if(forwardSpeed < 2f) forwardSpeed = 2f;
       if(forwardSpeed > 100f) forwardSpeed = 100f;

       // forwardSpeed -= transform.forward.y * 10f * Time.deltaTime;
       //rigidbody.AddForce(transform.forward * forwardSpeed * Time.deltaTime, ForceMode.VelocityChange);
        rigidbody.velocity += transform.forward * forwardSpeed * Time.deltaTime;

        //transform.position += transform.forward * speed * Time.deltaTime;
        //
        //// Adjust speed based on the way the plane is facing.

    }



    private void DropBomb(float value)
    {
        if(value > 0)
        {
            lastBomb = Instantiate(bombPrefab, this.transform.position, Quaternion.identity);
            bombCamera.UpdateTarget(lastBomb);
        }
    }


    private void BombCamera(float value)
    {        
        if(value > 0)
        {
            if(bombCamera.Toggle())
            {
                aircraftCamera.enabled = false;
            }
            else
            {
                aircraftCamera.enabled = true;
            }
        }
    }
}
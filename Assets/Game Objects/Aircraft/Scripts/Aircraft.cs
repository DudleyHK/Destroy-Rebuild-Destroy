using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Aircraft : MonoBehaviour 
{
    [SerializeField]
    private float minSpeed = 40f;
    [SerializeField]
    private float maxSpeed = 200f;
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
    [SerializeField]
    private ControllerID controllerID = ControllerID.Unassigned;
    [SerializeField]
    private ObjectType objectType = ObjectType.Unassigned;



    private new Rigidbody rigidbody;








    private void Awake()
    {
        rigidbody  = GetComponent<Rigidbody>();
        bombCamera = GetComponentInChildren<BombCamera>();
    }

    private void Start()
    {
        // TODO: Parse the tag into the Enum type and set the objectType as that.
    }


    private void OnEnable()
    {
        InputManager.inputDetected += HandleInput;
        PlayerManager.playerCreated += InsertPlayer;
    }


    private void OnDisable()
    {
        InputManager.inputDetected -= HandleInput;
        PlayerManager.playerCreated -= InsertPlayer;
    }


    private void Update()
    {
        //rigidbody.velocity += transform.forward * 20f * Time.deltaTime;
    }



    private void HandleInput(GameAction gameAction, float value, ControllerID ID)
    {
        Debug.Log("MESSAGE: " + gameAction.ToString());
        if(controllerID != ID) return;

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
    }


    private void InsertPlayer(ControllerID ID, ObjectType objectType)
    {
        if(objectType != this.objectType)
            return;
        controllerID = ID;
    }


    private void Pitch(float value)
    {
        rigidbody.angularVelocity += transform.right * (-value * angularSpeed) * Time.deltaTime;
    }

    private void Yaw(float value)
    {
        rigidbody.angularVelocity += transform.up * (value * angularSpeed) * Time.deltaTime;
    }

    private void Roll(float value)
    {
        rigidbody.angularVelocity += transform.forward * (-value * angularSpeed) * Time.deltaTime;
    }


    private void Forward(float value)
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.green, 5f);

        var velocity = transform.forward * (-value * speed) * Time.deltaTime;
        rigidbody.AddRelativeForce(velocity);
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
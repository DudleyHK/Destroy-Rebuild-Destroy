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
    private ControllerID controllerID = ControllerID.Unassigned;
    [SerializeField]
    private ObjectType objectType = ObjectType.Unassigned;

    private void Start()
    {

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


    private void HandleInput(GameAction gameAction, float value, ControllerID ID)
    {
        if(controllerID != ID) return;

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
                break;
            case GameAction.RS_X_Axis:
                break;
            case GameAction.RS_Y_Axis:
                break;
        }
    }


    private void InsertPlayer(ControllerID ID, ObjectType objectType)
    {
        if(objectType != this.objectType) return;
        controllerID = ID; 
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
}

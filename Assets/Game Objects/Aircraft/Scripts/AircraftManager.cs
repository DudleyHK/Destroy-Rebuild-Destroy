using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This handles the Input, Updates Components and knows who its controlled by. 
/// </summary>
public class AircraftManager : MonoBehaviour 
{
    [SerializeField]
    private AircraftPhysics aircraftPhysics;
    [SerializeField]
    private ControllerID controllerID = ControllerID.Unassigned;
    [SerializeField]
    private ObjectType objectType = ObjectType.Unassigned;




    private void Awake()
    {
        aircraftPhysics = GetComponent<AircraftPhysics>();
        if(!aircraftPhysics)
        {
            Debug.Log("ERROR: AircraftPhysics script is not found");
            return;
        }
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
        // Update Collisions
    }




    private void HandleInput(GameAction gameAction, float value, ControllerID ID)
    {
        if(controllerID != ID)
            return;

        switch(gameAction)
        {
            case GameAction.LS_X_Axis:
               aircraftPhysics.RollData(value);
                break;
            case GameAction.LS_Y_Axis:
                aircraftPhysics.PitchData(value);
                break;
            case GameAction.RT_Axis:
                aircraftPhysics.ThrustData(-value);
                break;
            case GameAction.LB_Held:
                aircraftPhysics.YawData(value);
                break;
            case GameAction.RB_Held:
                aircraftPhysics.YawData(value);
                break;
        }
    }



    private void InsertPlayer(ControllerID ID, ObjectType objectType)
    {
        if(objectType != this.objectType)
            return;
        controllerID = ID;
    }
}

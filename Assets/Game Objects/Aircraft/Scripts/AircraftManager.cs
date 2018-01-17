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
    private AircraftBombHatch aircraftBombHatch;
    [SerializeField]
    private AircraftFuelTank aircraftFuelTank;
    [SerializeField]
    private Camera aircraftCamera;
    [SerializeField]
    private ControllerID controllerID = ControllerID.Unassigned;
    [SerializeField]
    private ObjectType objectType = ObjectType.Unassigned;


    // TODO: Add fuel to the aircraft.
    // TODO: Count down Fuel.. Depending on the current velocity  lower it faster.


    private void Awake()
    {
        aircraftPhysics = GetComponent<AircraftPhysics>();
        if(!aircraftPhysics)
        {
            Debug.Log("ERROR: AircraftPhysics script is not found");
            return;
        }

        aircraftBombHatch = GetComponent<AircraftBombHatch>();
        if(!aircraftBombHatch)
        {
            Debug.Log("ERROR: AircraftBombHatch script is not found");
            return;
        }

        aircraftFuelTank = GetComponent<AircraftFuelTank>();
        if(!aircraftFuelTank)
        {
            Debug.Log("ERROR: AircraftFuelTank script is not found");
            return;
        }

        aircraftCamera = FindObjectOfType<AircraftCamera>().GetComponent<Camera>();
        if(!aircraftCamera)
        {
            Debug.Log("ERROR: AircraftCamera script is not found");
            return;
        }
    }
      
    private void Start()
    {
        // TODO: Parse the tag into the Enum type and set the objectType as that.
    }



    private void OnEnable()
    {
        InputManager.inputDetected  += HandleInput;
        PlayerManager.playerCreated += InsertPlayer;
        Runway.aircraftResupplied   += Resupply;
    }


    private void OnDisable()
    {
        InputManager.inputDetected  -= HandleInput;
        PlayerManager.playerCreated -= InsertPlayer;
        Runway.aircraftResupplied   -= Resupply;
    }


    private void Update()
    {
        aircraftFuelTank.UpdateMultiplier(aircraftPhysics.CurrentMagnitude);
        if(aircraftFuelTank.FuelTankEmpty)
        {
            Debug.Log("MESSAGE: Shutting off engines.");
        }
        else
        {
            // TODO: Set engines back online. 
        }
    }




    private void HandleInput(GameAction gameAction, float value, ControllerID ID)
    {
        if(controllerID != ID)
            return;

        switch(gameAction)
        {
            case GameAction.A_Down:
                aircraftBombHatch.DropBomb();
                break;
            case GameAction.B_Down:
                if(aircraftBombHatch.ToggleHatchCam(value))
                {
                    aircraftCamera.enabled = false;
                }
                else
                {
                    aircraftCamera.enabled = true;
                }
                break;
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


    private void Resupply()
    {
        aircraftBombHatch.Resupply();
        aircraftFuelTank.Resupply();
    }
}

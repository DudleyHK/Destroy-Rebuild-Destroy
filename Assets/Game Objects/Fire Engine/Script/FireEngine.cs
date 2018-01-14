using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: This class would be a fire engine manager ie log the posiitons which players are in, and each role.

// For this the fire engine does everything, later each person will have a different role based on which posiiton they're in.

public class FireEngine : MonoBehaviour 
{


    private void OnEnable()
    {
        InputManager.InputDetected += HandleInput;
    }


    private void OnDisable()
    {
        InputManager.InputDetected -= HandleInput;
    }


    private void HandleInput(GameAction gameAction, float value, int ID)
    {
        // if (myID = ID)
        switch(gameAction)
        {
            case GameAction.Horizontal:
                // 
                break;
            case GameAction.Vertical:
                break;
            case GameAction.DriveMeHard:
                break;
            case GameAction.Action:
                break;
            case GameAction.AntiAction:
                break;
            case GameAction.None:
                break;
            default:
                break;
        }
    }


}

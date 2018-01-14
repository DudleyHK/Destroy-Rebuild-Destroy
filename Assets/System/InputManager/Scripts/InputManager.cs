using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameAction
{
    LS_X_Axis,
    LS_Y_Axis,
    RS_X_Axis,
    RS_Y_Axis,
    LT_Axis,
    RT_Axis,
    LB_Down,
    LB_Held,
    RB_Down,
    RB_Held,
    A_Down,
    A_Held,
    B_Down,
    B_Held,
    None
}


public class InputManager : MonoBehaviour
{
    public delegate void InputAxis(GameAction gameAction, float value ,int ID);
    public static event  InputAxis InputDetected;


    private const int MAX_PLAYERS = 4;
    private int gamepadID = 1;


    private void Update()
    {
        for(gamepadID = 1; gamepadID <= MAX_PLAYERS; gamepadID++)
        {
            AllAxis();
            AllButtons();
        }
    }



    private void AllAxis()
    {
        LeftStick();
        RightStick();
        Triggers();
    }

    private void LeftStick()
    {
        var x = Input.GetAxis("Controller " + gamepadID + " - Horizontal");
        InputDetected(GameAction.LS_X_Axis, x, gamepadID);

        var y = Input.GetAxis("Controller " + gamepadID + " - Vertical");
        InputDetected(GameAction.LS_Y_Axis, y, gamepadID);
    }

    private void RightStick()
    {
        var x = Input.GetAxis("Controller " + gamepadID + " - Second Horizontal");
        InputDetected(GameAction.RS_X_Axis, x, gamepadID);

        var y = Input.GetAxis("Controller " + gamepadID + " - Second Vertical");
        InputDetected(GameAction.RS_Y_Axis, y, gamepadID);
    }
  
    private void Triggers()
    {
        var trigger = Input.GetAxis("Controller " + gamepadID + " - Trigger");
       
        if(trigger > 0)
        {
            InputDetected(GameAction.LT_Axis, trigger, gamepadID);
        }
        else if (trigger < 0)
        {
            InputDetected(GameAction.RT_Axis, trigger, gamepadID);
        }
    }




    private void AllButtons()
    {
        ADown();
        AHeld();

        BDown();
        BHeld();

        LBHeld();
        RBHeld();
    }

    private void ADown()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - A");
        if(action)
        {
            InputDetected(GameAction.A_Down, 1, gamepadID);
        }
    }

    private void AHeld()
    {
        var action = Input.GetButton("Controller " + gamepadID + " - A");
        if(action)
        {
            InputDetected(GameAction.A_Held, 1, gamepadID);
        }
    }

    private void BDown()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - B");
        if(action)
        {
            InputDetected(GameAction.B_Down, 1, gamepadID);
        }
    }

    private void BHeld()
    {
        var action = Input.GetButton("Controller " + gamepadID + " - B");
        if(action)
        {
            InputDetected(GameAction.B_Held, 1, gamepadID);
        }
    }

    private void LBDown()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - LeftBumper");
        if(action)
        {
            InputDetected(GameAction.LB_Down, -1, gamepadID);
        }
    }

    private void LBHeld()
    {        
        var action = Input.GetButton("Controller " + gamepadID + " - LeftBumper");
        if(action)
        {
            InputDetected(GameAction.LB_Held, -1, gamepadID);
        }
    }

    private void RBDown()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - RightBumper");
        if(action)
        {
            InputDetected(GameAction.RB_Down, 1, gamepadID);
        }
    }

    private void RBHeld()
    {
        var action = Input.GetButton("Controller " + gamepadID + " - RightBumper");
        if(action)
        {
            InputDetected(GameAction.RB_Held, 1, gamepadID);
        }
    }
}

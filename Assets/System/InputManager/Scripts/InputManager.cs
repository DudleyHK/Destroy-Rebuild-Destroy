using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameAction
{
    Horizontal,
    Vertical,
    DriveMeHard,
    RightBumper,
    LeftBumper,
    Action,
    AntiAction,
    None
}


public class InputManager : MonoBehaviour
{
    public delegate void InputAxis(GameAction gameAction, float value ,int ID);
    public static event  InputAxis InputDetected;


    //public delegate void InputButton(GameAction gameAction, int ID);
    //public static event InputButton inputButton;

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
        Horizontal();
        Vertical();
        RightTrigger();
    }

    private void Horizontal()
    {
        var x = Input.GetAxis("Controller " + gamepadID + " - Horizontal");
        InputDetected(GameAction.Horizontal, x, gamepadID);
    }

    private void Vertical()
    {
        var y = Input.GetAxis("Controller " + gamepadID + " - Vertical");
        InputDetected(GameAction.Vertical, y, gamepadID);
    }

    private void RightTrigger()
    {
        var rt = Input.GetAxis("Controller " + gamepadID + " - RightTrigger");
        InputDetected(GameAction.DriveMeHard, Mathf.Abs(rt), gamepadID);
    }


    private void AllButtons()
    {
        Action();
        AntiAction();
        LeftBumper();
        RightBumper();
    }


    private void Action()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - A");
        if(action)
        {
            InputDetected(GameAction.Action, 1, gamepadID);
        }
        //else
        //{
        //    InputDetected(GameAction.Action, 0, gamepadID);
        //}
    }


    private void AntiAction()
    {
        var action = Input.GetButtonDown("Controller " + gamepadID + " - B");
        if(action)
        {
            InputDetected(GameAction.AntiAction, 1, gamepadID);
        }
       //else
       //{
       //    InputDetected(GameAction.AntiAction, 0, gamepadID);
       //}
    }


    private void LeftBumper()
    {        
        var action = Input.GetButton("Controller " + gamepadID + " - LeftBumper");
        if(action)
        {
            InputDetected(GameAction.LeftBumper, -1, gamepadID);
        }
        //else
        //{
        //    InputDetected(GameAction.LeftBumper, 0, gamepadID);
        //}
    }

    private void RightBumper()
    {
        var action = Input.GetButton("Controller " + gamepadID + " - RightBumper");
        if(action)
        {
            InputDetected(GameAction.RightBumper, 1, gamepadID);
        }
       //else
       //{
       //    InputDetected(GameAction.RightBumper, 0, gamepadID);
       //}
    }



}

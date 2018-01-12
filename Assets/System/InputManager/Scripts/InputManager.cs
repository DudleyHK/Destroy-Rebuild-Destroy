using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameAction
{
    Horizontal,
    Vertical,
    DriveMeHard,
    RightButton,
    LeftButton,
    A,
    B,
    None
}


public class InputManager : MonoBehaviour
{
    public delegate void InputAxis(GameAction gameAction, float value ,int ID);
    public static event  InputAxis inputAxis;


    public delegate void InputButton(GameAction gameAction, int ID);
    public static event InputButton inputButton;

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
        inputAxis(GameAction.Horizontal, x, gamepadID);
    }

    private void Vertical()
    {
        var y = Input.GetAxis("Controller " + gamepadID + " - Vertical");
        inputAxis(GameAction.Vertical, y, gamepadID);
    }

    private void RightTrigger()
    {
        var rt = Input.GetAxis("Controller " + gamepadID + " - RightTrigger");
        inputAxis(GameAction.DriveMeHard, Mathf.Abs(rt), gamepadID);
    }


    private void AllButtons()
    {

    }



}

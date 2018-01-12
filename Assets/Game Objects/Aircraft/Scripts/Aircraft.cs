using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float angularSpeed = 0.1f;




    private new Rigidbody rigidbody;






    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        InputManager.inputAxis += InputAxis;
        InputManager.inputButton += InputButton;
    }


    private void OnDisable()
    {
        InputManager.inputAxis -= InputAxis;
        InputManager.inputButton -= InputButton;
    }




    private void InputAxis(GameAction gameAction, float value, int ID)
    {
        // if (myID == ID)
        // {

        switch(gameAction)
        {
            case GameAction.Horizontal:
                Roll(value);
                break;
            case GameAction.Vertical:
                Pitch(value);
                break;
            case GameAction.DriveMeHard:
                Forward(value);
                break;
        }

        // }
    }


    private void Pitch(float value)
    {
        rigidbody.angularVelocity += transform.right * (-value * angularSpeed) * Time.deltaTime;
    }

    private void Yaw(float value)
    {
        // TODO: Use right/ left bumper.
    }

    private void Roll(float value)
    {
        rigidbody.angularVelocity += transform.forward * (-value * angularSpeed) * Time.deltaTime;
    }


    private void Forward(float value)
    {
        rigidbody.velocity += transform.forward * (value * speed) * Time.deltaTime;
    }


    private void InputButton(GameAction gameAction, int ID)
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WaterCanon : MonoBehaviour 
{
    [SerializeField]
    private float fastGush = 100f;
    [SerializeField]
    private float slowGush = 50f;
    [SerializeField]
    private float spinSpeed = 50f;
    [SerializeField]
    private float liftSpeed = 40f;
    [SerializeField]
    private float minLift = 25f;
    [SerializeField]
    private float maxLift = 125f;
    [SerializeField]
    private GameObject rotationBox;
    [SerializeField]
    private GameObject hose;
    [SerializeField]
    private GameObject waterPrefab;
    [SerializeField]
    private ControllerID controllerID = ControllerID.Unassigned;
    [SerializeField]
    private ObjectType objectType = ObjectType.Unassigned;
    [SerializeField]
    private int canonID = -1;



    private void Start()
    {
        if(canonID == -1)
        {
            Debug.Log("ERROR: Canon ID has not been set. Set ID to canon number");
            return;
        }


        rotationBox = GameObject.Find("Rotation Box - " + canonID);
        if(!rotationBox)
        {
            Debug.Log("ERROR: Rotation box object not found.");
            return;
        }

        hose = GameObject.Find("Hose - " + canonID);
        if(!hose)
        {
            Debug.Log("ERROR: Child GameObject (hose) not found");
            return;
        }
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
            case GameAction.LS_X_Axis:
                Spin(value);
                break;
            case GameAction.LS_Y_Axis:
                Lift(value);
                break;
            case GameAction.A_Held:
                SprayWater(value);
                break;
            default:
                break;
        }

    }



    private void InsertPlayer(ControllerID ID, ObjectType objectType)
    {
        if(objectType != this.objectType)
            return;
        controllerID = ID;
    }


    private void Spin(float value)
    {
        rotationBox.transform.Rotate(0f, value * spinSpeed * Time.deltaTime, 0f, Space.Self);
    }



    private void Lift(float value)
    {
        // TODO: Limit the rotation of hose...
        // https://gamedev.stackexchange.com/questions/109974/unity-problem-in-limiting-rotation-of-object/109980
        hose.transform.Rotate(value * liftSpeed * Time.deltaTime, 0f, 0f, Space.Self);
    }


    private void SprayWater(float value)
    {
        var spawnPoint = hose.transform.position + (hose.transform.up * 10f);

        var clone = Instantiate(waterPrefab, spawnPoint, Quaternion.identity);
        var clone2 = Instantiate(waterPrefab, spawnPoint, Quaternion.identity);

        clone.GetComponent<Rigidbody>().velocity += hose.transform.up * fastGush;
        clone2.GetComponent<Rigidbody>().velocity += hose.transform.up * slowGush;


        Debug.DrawRay(hose.transform.position, hose.transform.up * 10f, Color.green, 5f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Data is passed from the AircraftManager but Physics Update is always running. 
/// </summary>
public class AircraftPhysics : MonoBehaviour
{
    //"Core Movement", "Controls for the various speeds for different operations."
    [SerializeField]
    private float speed                 = 20f; //"Base Speed", "Primary flight speed, without afterburners or brakes"
    [SerializeField]
    private float afterburnerSpeed      = 40f; //Afterburner Speed", "Speed when the button for positive thrust is being held down"
    [SerializeField]
    private float slowSpeed             = 4f;  //"Brake Speed", "Speed when the button for negative thrust is being held down"
    [SerializeField]
    private float thrustTransitionSpeed = 5f;  //Thrust Transition Speed", "How quickly afterburners/brakes will reach their maximum effect"
    [SerializeField]
    private float turnSpeed             = 15f; //"Turn/Roll Speed", "How fast turns and rolls will be executed "
    [SerializeField]
    private float rollSpeedModifier     = 7f;  //"Roll Speed", "Multiplier for roll speed. Base roll is determined by turn speed"
    [SerializeField]
    private float pitchYawModifier      = 15f; //"Pitch/Yaw Multiplier", "Controls the intensity of pitch and yaw inputs"
    [SerializeField]
    private float gravitationalModifier = 10f;  //"Gravitational Multiplier", "Controls the speed of the aircraft when lifting and dipping its nose"
    [SerializeField]
    private float gravityScale          = 15f;  //"Gravity", "A downwards force effecting the aircraft"

    //"Banking", "Visuals only--has no effect on actual movement"
    [SerializeField]
    private float bankAngleClamp         = 360f; //"Bank Angle Clamp", "Maximum angle the spacecraft can rotate along the Z axis."
    [SerializeField]
    private float bankRotationSpeed      = 3f;   //"Bank Rotation Speed", "Rotation speed along the Z axis when yaw is applied. Higher values will result in snappier banking."
    [SerializeField]
    private float bankRotationMultiplier = 1f;   //"Bank Rotation Multiplier", "Bank amount along the Z axis when yaw is applied."


    private GameObject aircraftObject;
    private new Rigidbody rigidbody;

    private float thrustValue = 0f;
    private float pitchValue  = 0f;
    private float yawValue    = 0f;
    private float rollValue   = 0f;

    private float currentMagnitude = 0f;
    

    public bool  AfterBurnerActive { get; private set; }
    public float Yaw { get; private set; }



    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if(!rigidbody)
        {
            Debug.Log("ERROR: Rigidbody Component cannot be found");
            return;
        }

        aircraftObject = rigidbody.gameObject;
        if(!aircraftObject)
        {
            Debug.LogError("(AircraftPhysics) Aircraft GameObject is null.");
            return;
        }

        AfterBurnerActive = false;
    }


    private void FixedUpdate()
    {
        UpdateForces();
    }




    /// <summary>
    /// Update the forces applied to the Aircraft. Use fixedDeltaTime.
    /// </summary>
    private void UpdateForces()
    {
        var roll  = rollValue * -rollSpeedModifier;
        var pitch = pitchValue * pitchYawModifier;
        
        Yaw = yawValue * pitchYawModifier;
        currentMagnitude = rigidbody.velocity.magnitude;


        if(thrustValue > 0)
        {
            //If input on the thrust axis is positive, activate afterburners.
            AfterBurnerActive = true;
            currentMagnitude = Mathf.Lerp(currentMagnitude, afterburnerSpeed, thrustTransitionSpeed * Time.fixedDeltaTime);
        }
        else if(thrustValue < 0)
        {   
            //If input on the thrust axis is negatve, activate brakes.
            AfterBurnerActive = false;
            currentMagnitude = Mathf.Lerp(currentMagnitude, slowSpeed, thrustTransitionSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //Otherwise, hold normal speed.
            AfterBurnerActive = false;
            currentMagnitude = Mathf.Lerp(currentMagnitude, speed, thrustTransitionSpeed * Time.fixedDeltaTime);
        }

        rigidbody.AddRelativeTorque(
            (pitch * turnSpeed * Time.fixedDeltaTime),
            (Yaw   * turnSpeed * Time.fixedDeltaTime),
            (roll  * turnSpeed * (rollSpeedModifier / 2f) * Time.fixedDeltaTime));


        currentMagnitude -= transform.forward.y * gravitationalModifier;

        rigidbody.AddForce(Vector3.down * gravityScale * (rigidbody.mass / Physics.gravity.magnitude));// * (, ForceMode.Force);

        rigidbody.velocity = transform.forward * currentMagnitude;
        
        //UpdateBanking();
    }


    void UpdateBanking()
    {
        //Load rotation information.
        Quaternion newRotation = transform.rotation;
        Vector3 newEulerAngles = newRotation.eulerAngles;

        //Basically, we're just making it bank a little in the direction that it's turning.
        newEulerAngles.z += Mathf.Clamp((-Yaw * turnSpeed * Time.fixedDeltaTime) * bankRotationMultiplier, -bankAngleClamp, bankAngleClamp);
        newRotation.eulerAngles = newEulerAngles;

        //Apply the rotation to the gameobject that contains the model.
        aircraftObject.transform.rotation = Quaternion.Slerp(aircraftObject.transform.rotation, newRotation, bankRotationSpeed * Time.fixedDeltaTime);
    }


    public void ThrustData(float value)
    {
        thrustValue = value;
    }

    public void PitchData(float value)
    {
        pitchValue = value;
    }

    public void YawData(float value)
    {
        yawValue = value;
    }

    public void RollData(float value)
    {
        rollValue = value;
    }
}
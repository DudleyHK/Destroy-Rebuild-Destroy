using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftBombHatch : MonoBehaviour 
{
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private float HATCH_LOCK_TIME = 3f; 
    [SerializeField]
    private float timer = 0f;
    [SerializeField]
    private bool hatchLocked = false;
    [SerializeField]
    private int defaultStock = 5;
    [SerializeField]
    private int bombStock = 0;

    [SerializeField]
    private Camera hatchCam;
    [SerializeField]
    private bool active = false;
    [SerializeField]
    private float cameraX = 0f;
    [SerializeField]
    private float cameraZ = 7.5f;
    [SerializeField]
    private float hatchCamShakeAmount = 2.5f;
    [SerializeField]
    private float hatchCamRotationTightness = 5f;



    private void Start()
    {
        bombStock = defaultStock;
        hatchCam = GetComponentInChildren<Camera>();
        if(!hatchCam)
        {
            Debug.Log("ERROR: HatchCam component not found in child object.");
        }
        active = false;
    }


    private void Update()
    {
        if(hatchLocked)
        {
            if(timer <= HATCH_LOCK_TIME)
            {
                timer += Time.deltaTime;
            }
            else
            {
                hatchLocked = false;
                timer = 0f;
            }
        }

        hatchCam.enabled = active;

        var direction = hatchCam.transform.forward;
        var newRotation = Quaternion.LookRotation(direction + new Vector3(
            Random.Range(-hatchCamShakeAmount, hatchCamShakeAmount), 
            Random.Range(-hatchCamShakeAmount, hatchCamShakeAmount),
            Random.Range(-hatchCamShakeAmount, hatchCamShakeAmount)),
            hatchCam.transform.up);

        hatchCam.transform.rotation = Quaternion.Slerp(hatchCam.transform.rotation, newRotation, Time.deltaTime * hatchCamRotationTightness);
    }


    public void DropBomb()
    {
        if(!hatchLocked && bombStock > 0)
        {
            var bomb = Instantiate(bombPrefab, transform.position + (transform.forward * 25f), Quaternion.identity);
            hatchLocked = true;
            bombStock--;
        }
    }

    /// <summary>
    /// Toggle the hatch camera on/ off.
    /// </summary>
    /// <returns></returns>
    public bool ToggleHatchCam(float value)
    {
        if(value > 0)
            active = !active;
        return active;
    }


    public void Resupply()
    {
        bombStock = defaultStock;
    }
}

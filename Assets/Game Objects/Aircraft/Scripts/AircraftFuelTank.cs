using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftFuelTank : MonoBehaviour 
{
    [SerializeField]
    private float defaultFuelSupply = 10000f;
    [SerializeField]
    private float fuelSupply = 0;
    [SerializeField]
    private float speedMultiplier = 0f;

    public bool FuelTankEmpty { get; private set; }



    private void Start()
    {
        Resupply();
    }



    private void Update()
    {
        if(fuelSupply > 0f)
        {
            fuelSupply -= speedMultiplier * Time.deltaTime;
            FuelTankEmpty = false;
        }
        else
        {
            Debug.Log("MESSAGE: Warning aircraft has run out of fuel.");
            FuelTankEmpty = true;
        }
    }


    public void UpdateMultiplier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }


    public void Resupply()
    {
        fuelSupply = defaultFuelSupply;
    }
}

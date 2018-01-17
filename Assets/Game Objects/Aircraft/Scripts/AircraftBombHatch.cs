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



    // TODO: Add switch to bomb hatch cam.


    private void Start()
    {
        bombStock = defaultStock;
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
    }


    public void DropBomb()
    {
        if(!hatchLocked && bombStock > 0)
        {
            var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            hatchLocked = true;
            bombStock--;
        }
    }


    public void Resupply()
    {
        bombStock = defaultStock;
    }
}

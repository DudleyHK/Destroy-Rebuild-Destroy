using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftBombHatch : MonoBehaviour 
{
    [SerializeField]
    private GameObject bombPrefab;



    // TODO: Add switch to bomb hatch cam. 
    // TODO: Limit the number of bombs allowed to be dropped. 


    private void Start()
    {
        
    }



    public void DropBomb()
    {
        var bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}

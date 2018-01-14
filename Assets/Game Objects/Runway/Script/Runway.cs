using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runway : MonoBehaviour 
{
    [SerializeField]
    private Renderer[] signalLightRenderers;
    [SerializeField]
    private bool recharging = false;
    [SerializeField]
    private float MAX_TIME = 5f;
    [SerializeField]
    private float timer = 0f;



    private void Start()
    {
        var signalLight = GameObject.FindGameObjectsWithTag("SignalLight");
        if(signalLight == null)
        {
            Debug.Log("ERROR: Signal light tag not found");
            return;
        }

        signalLightRenderers = new Renderer[2];
        for(int i = 0; i < signalLightRenderers.Length; i++)
        {
            signalLightRenderers[i] = signalLight[i].GetComponent<Renderer>();
            signalLightRenderers[i].material.color = Color.red;
        }
    }


    private void Update()
    {
        if(recharging)
        {
            if(timer < MAX_TIME)
            {
                timer += Time.deltaTime;
            }
            else
            {
                StartCoroutine(ColourSwitch());
            }
        }
        else
        {
            timer = 0f;
        }
    }

    // TODO: When plane is in the collision box. turn houses green. This can be changed to a more complex system later.
    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if(otherObject.tag == "Aircraft")
        {
            recharging = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        var otherObject = other.gameObject;
        if(otherObject.tag == "Aircraft")
        {
            recharging = false;
        }
    }



    private IEnumerator ColourSwitch()
    {
        float MAX_TIME = 10f;
        float timer = 0f;
        while(timer < MAX_TIME)
        {
            for(int i = 0; i < signalLightRenderers.Length; i++)
            {
                signalLightRenderers[i].material.color = Color.green;
            }
            timer += Time.deltaTime;
            yield return false;
        }
        for(int i = 0; i < signalLightRenderers.Length; i++)
        {
            signalLightRenderers[i].material.color = Color.red;
        }
        yield return true;
    }
}

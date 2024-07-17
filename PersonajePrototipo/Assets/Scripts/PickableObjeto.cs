using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjeto : MonoBehaviour
{

    public bool IsPickable = true;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "PlayerZonaInt")
        {
            other.GetComponentInParent<PickPlayer>().ObjectToPickup = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag== "PlayerZonaInt")
        {
            other.GetComponentInParent<PickPlayer>().ObjectToPickup = null;
        }
    }
}

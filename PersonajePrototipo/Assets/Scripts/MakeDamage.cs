using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int cantidad = 20;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
            {
            other.GetComponent<VidaYDaño>().RestarVida(cantidad);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<VidaYDaño>().RestarVida(cantidad);
        }

    }
}

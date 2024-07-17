using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    public AudioSource quienEmite;
    public AudioClip elSonido;
    public float volumen = 1f;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        quienEmite.PlayOneShot(elSonido, volumen);
    }
}

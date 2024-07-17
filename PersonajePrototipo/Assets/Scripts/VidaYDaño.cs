using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaYDaño : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida = 100;
    public bool invencible = false;
    public float tiempo_invencible = 1f;
    public float Tiempo_frenado = 0.2f;
    public float Tiempo_Muerto = 3f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void RestarVida(int cantidad)
    {
        if (!invencible && vida>0)
        {
            vida -= cantidad;
            anim.Play("Daño");
            StartCoroutine(Invunerabilidad());
            StartCoroutine(FrenarVelocidad());

            if (vida == 0)
            {
                anim.Play("Dead");
                StartCoroutine(Dead());
                GameOver();
            }
        }
        
    }

    void GameOver()
    {

        Debug.Log("GAME OVER");
        Time.timeScale= 0;
    }

    IEnumerator Invunerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempo_invencible);
        invencible = false;
    }

    IEnumerator FrenarVelocidad()
    {
        var velocidadActual = GetComponent<Controller>().playerSpeed;
        GetComponent<Controller>().playerSpeed=0;
        yield return new WaitForSeconds(Tiempo_frenado);
        GetComponent<Controller>().playerSpeed = velocidadActual;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(Tiempo_Muerto);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerPlat : MonoBehaviour
{
    public CharacterController player;  //Variable para almacenar el character controller
    public Vector3 groundposition;      //Almacenamos la posicion actual del suelo
    public Vector3 lastgroundposition;  //Almacenamos la ultima posicion conocida del suelo
    public string groundName;           //Almacenamos el nombre actual del suelo
    public string lastgroundName;       //Almacenamos el nombre del ultimo suelo conocido

    Quaternion actualRot;
    Quaternion lastRot;

    public Vector3 originOffset;
    public float factorDivision = 4.2f;
    private bool isMoving = false; // Nueva variable para controlar el movimiento

    void Start()
    {
        player = this.GetComponent<CharacterController>(); //Inicializamos la variable player almacenando el componente CharacterController
    }

    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Desactivar el CharacterController si el suelo se llama "plataforma" y no se est� moviendo
        if (groundName == "Plataforma" && !isMoving)
        {
            player.enabled = false;
        }
        else
        {
            player.enabled = true;
        }
        */
        if (player.isGrounded)      //En casi de estar tocando el suelo
        {
            RaycastHit hit;         //Creamos una variable para almacenar la colision del RayCast
            if (Physics.SphereCast(transform.position + originOffset, player.radius / factorDivision, -transform.up, out hit))   //Y creamos una SphereCast, que es como un RayCast pero grueso, dandole un diametro y una direccion en la que "disparar" ese SphereCast
            {
                GameObject groundedIn = hit.collider.gameObject;    //Comprobmamos cual es el suelo actual y almacenamos su GameObject en una variable temporal
                groundName = groundedIn.name;                       //despues comprobamos el nombre del suelo
                groundposition = groundedIn.transform.position;     //Una vez que tenemos el GameObject localizado, almacenamos su posicion

                actualRot = groundedIn.transform.rotation;
                if (groundposition != lastgroundposition && groundName == lastgroundName)   //Si su posicion es distinta de la ultima posicion conocida y el nombre sigue siendo el mismo
                {

                    this.transform.position += groundposition - lastgroundposition; //Sumamos a nuestra posicion la diferencia entre la posicion actual del suelo y la ultimam posicion conocida del suelo.
                    Vector3 platformVelocity = (groundposition - lastgroundposition) / Time.deltaTime;
                    player.Move(platformVelocity * Time.deltaTime);
                }
                if (actualRot != lastRot && groundName == lastgroundName) //hacemos que la roatcion del personaje se sincronize con la del suelo.
                {
                    var newRot = this.transform.rotation * (actualRot.eulerAngles - lastRot.eulerAngles);  //almacenamos los grados que hay de diferencia entre la rotacion actual y la ultima conozida de la plataforma, y lo multiplicamos por la rotacion del jugador
                    this.transform.RotateAround(groundedIn.transform.position, Vector3.up, newRot.y);     //ahora rotamos al jugador al rededor de la posicion de la plataforma, en el eje Y los grados almacenados en la anterior variable. 

                }
                lastgroundName = groundName;        //Asignamos al ultimo suelo conocido, tanto el nombre como la posicion del suelo actual.
                lastgroundposition = groundposition; //asignamos la ultima posicion conocida de la plataforma
                lastRot = actualRot;                //asignamos la ultima rotacion conocida de la plataforma

            }
        }
        /*
        else if (player.isGrounded == false) //Si no estamos tocando el suelo reseteamos todas las variables a 0 para que no tengamos problemas al saltar estando en la plataforma.
        {
            lastgroundName = null;
            lastgroundposition = Vector3.zero;
            lastRot = Quaternion.Euler(0, 0, 0);
        }
        */

    }
    //Aqui unicamente creamos un gizmo para poder comprobar si el diametro del SphereCast es adecuado al tama�o de nuestro jugador.
    private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + originOffset, player.radius / factorDivision);
    }
}

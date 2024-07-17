using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ObjectToPickup;
    public GameObject PickedObject;
    public Transform zonaInt;

    void Update()
    {
        if (ObjectToPickup != null && ObjectToPickup.GetComponent<PickableObjeto>().IsPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickedObject = ObjectToPickup;
                PickedObject.GetComponent<PickableObjeto>().IsPickable = false;
                PickedObject.transform.SetParent(zonaInt);
                PickedObject.transform.position = zonaInt.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;

            }
        }
        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickedObject.GetComponent<PickableObjeto>().IsPickable = true;
                PickedObject.transform.SetParent(null);
                PickedObject.GetComponent<Rigidbody>().useGravity = true;
                PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;

            }
        }
    }
}

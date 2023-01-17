using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamara : MonoBehaviour
{
    public GameObject John;

    // Update is called once per frame
    void Update()
    {
        if (John != null)
        {       
        //movemos la camara dependiendo de donde este el personaje gracias a la referencia de GameObject
        Vector3 position = transform.position;
        position.x = John.transform.position.x;
        transform.position = position;
        }
    }
}

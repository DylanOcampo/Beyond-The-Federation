using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMecanica : MonoBehaviour
{
    //Este codigo es para que cuando el jugador agarre la linterna, se active el codigo que permita utilizar la mecanica de la linterna
    //Agregar aqui la activacion del codigo de la linterna

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {

        }
    }

}

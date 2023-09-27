using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMG01 : MonoBehaviour
{
    public GameObject minijuego01;
    public GameObject limpiaflechas;
    public PlayerMovement jugador;


    private void Start()
    {
        desactivarMinijuego();
    }

    public void activarMinijuego()
    {
        minijuego01.SetActive(true);
        limpiaflechas.SetActive(false);
        jugador.enabled = false;
    }
    
    public void desactivarMinijuego()
    {
        minijuego01.SetActive(false);
        limpiaflechas.SetActive(true);
        jugador.enabled = true;
    }

}

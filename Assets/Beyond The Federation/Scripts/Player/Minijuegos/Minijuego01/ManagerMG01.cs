using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMG01 : MonoBehaviour
{
    public GameObject minijuego01;
    public GameObject limpiaflechas;

    public void activarMinijuego()
    {
        minijuego01.SetActive(true);
        limpiaflechas.SetActive(false);
        PlayerManagerControllers.instance.LockPlayerControl();
    }
    
    public void desactivarMinijuego()
    {
        minijuego01.SetActive(false);
        limpiaflechas.SetActive(true);
        PlayerManagerControllers.instance.LockPlayerControl();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDesactivarUI : MonoBehaviour
{
    public GameObject Coins;
    public GameObject Minimap;

    private bool activoinventario = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            activoinventario =! activoinventario;
        }

        if (activoinventario)
        {
            Coins.SetActive(false);
            Minimap.SetActive(false);
        }
        else
        {
            Coins.SetActive(true);
            Minimap.SetActive(true);
        }
    }

}

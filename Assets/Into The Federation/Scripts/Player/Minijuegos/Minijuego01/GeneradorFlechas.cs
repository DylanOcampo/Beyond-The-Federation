using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorFlechas : MonoBehaviour
{
    public GameObject[] flechas;
    private float tiempoEntreFlechas;
    public float comienzoTiempo;

    void Update()
    {
        if(tiempoEntreFlechas <= 0)
        {
            int random = Random.Range(0, flechas.Length);
            Instantiate(flechas[random], transform.position, Quaternion.identity);

            tiempoEntreFlechas = comienzoTiempo;
        }
        else
        {
            tiempoEntreFlechas -= Time.deltaTime;
        }
    }
}

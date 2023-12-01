using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlechaIzq : MonoBehaviour
{
    public float velocidad;
    private int contador = 0;
    private bool adentro = false;

    private void Start()
    {
        transform.Rotate(0, 90, 0);
    }

    void Update()
    {
        transform.position += transform.up * -velocidad * Time.deltaTime;

        if (contador == 2)
        {
            adentro = true;
        }
        else
        {
            adentro = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (adentro)
            {
                GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().puntaje++;
                GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().scoreText.text = "Score: " + GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().puntaje.ToString();
                Destroy(gameObject);
            }
        }

        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "casillaTag")
        {
            contador++;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "casillaTag")
        {
            contador--;
        }
    }
}

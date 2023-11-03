using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaIzq : MonoBehaviour
{
    public float velocidad;
    private int contador = 0;
    private bool adentro = false;


    void Update()
    {

        transform.position += transform.right * -velocidad * Time.deltaTime;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "casillaTag")
        {
            contador++;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "casillaTag")
        {
            contador--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flechas")
        {
            GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().puntaje = 0;
            GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().scoreText.text = "Score: " + GameObject.Find("CasillaJugador").GetComponent<CasillaPlayer>().puntaje.ToString();
            GameObject.Find("ManagerMiniJuego01").GetComponent<ManagerMG01>().desactivarMinijuego();
        }
    }

}

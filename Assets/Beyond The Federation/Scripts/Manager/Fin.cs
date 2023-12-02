using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin : MonoBehaviour
{
    public GameObject EndCanvas;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EndCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

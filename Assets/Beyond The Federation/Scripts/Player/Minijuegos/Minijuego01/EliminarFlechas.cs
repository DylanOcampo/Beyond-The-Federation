using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EliminarFlechas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flechas")
        {
            Destroy(collision.gameObject);
        }
    }
}

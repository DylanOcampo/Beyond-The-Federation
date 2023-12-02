using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    Rigidbody rb;
    IEnumerator CheckCo;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Roier")
        {
            if (other.gameObject.GetComponent<RoierPlayer>().isPushing)
            {
                rb.mass = 1;
            }
            else
            {
                if(CheckCo == null)
                {
                    CheckCo = Check(other.gameObject);
                    StartCoroutine(CheckCo);
                }
                
            }
        }
    }

    IEnumerator Check(GameObject other)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (other.GetComponent<RoierPlayer>().isPushing)
            {
                rb.mass = 1;

            }
            if (!other.GetComponent<RoierPlayer>().isPushing && rb.mass == 1)
            {
                rb.mass = 100;

            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.mass = 100;
            if(CheckCo != null)
            {

                StopCoroutine(CheckCo);
                CheckCo = null;
            }
        }
    }

}

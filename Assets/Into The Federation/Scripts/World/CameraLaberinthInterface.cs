using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaberinthInterface : MonoBehaviour
{
    bool DoOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (!DoOnce && other.tag == "Player")
        {
            CamaraMovementManager.instance.ChangeCameraLaberinth();
            DoOnce = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        DoOnce = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaberinthInterface : MonoBehaviour
{
    bool DoOnce;
    public bool AMIlAB;
    public bool AMIExiting;
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
        if (  AMIlAB && other.tag == "Player")
        {
            CamaraMovementManager.instance.ChangeCameraLaberinth();
            
        }

        if (!AMIlAB && other.tag == "Player")
        {
            Debug.Log("asdf");
            CamaraMovementManager.instance.ChangeCameraNormal(AMIExiting);


        }



    }

    

}

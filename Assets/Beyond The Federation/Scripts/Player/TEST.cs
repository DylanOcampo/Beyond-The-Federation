using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    
    public Transform Player, Camara, CamaraTarget;
    private Vector3 offset;
    public float distanceFromObject = 5f;
    public float mouseSensitivity = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        offset = Player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CamaraTarget.transform.LookAt(Camara.transform);
        Debug.Log(CamaraTarget.transform.eulerAngles);
        Player.transform.eulerAngles = new Vector3(Player.transform.eulerAngles.x, CamaraTarget.transform.eulerAngles.y, Player.transform.eulerAngles.z);

        Camara.transform.LookAt(Player.transform);
        transform.position = Player.position - offset;

        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, 0, 45);

        Quaternion rotation =  Quaternion.Euler(rotationX, rotationY, 0f);

        Camara.transform.rotation = rotation;




    }
    void LateUpdate()
    {
        
        
            
          
        if (Player != null)
        {
            Camara.transform.position = Player.position - Camara.transform.forward * distanceFromObject;
        }
            
        
    }
}

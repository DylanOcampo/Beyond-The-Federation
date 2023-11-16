using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public GameObject ActualCamera;
    public PostProcessVolume postv;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            PostProcessingManager.instance.ChangeConfiginGameScene(ActualCamera, postv);
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("Start in Main Menu");
        }
        
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset;
    }
}

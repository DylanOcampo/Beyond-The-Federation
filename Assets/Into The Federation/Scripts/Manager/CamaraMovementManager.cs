using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovementManager : MonoBehaviour
{
    private static CamaraMovementManager _instance;
    public static CamaraMovementManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CamaraMovementManager>();
            }
            return _instance;
        }
    }

    public Transform Player, LaberinthTarget;
    public GameObject Camera;
    
    private Vector3 offset;

    public bool CanFollowPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        CalculateOffset();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    public void ChangeCameraLaberinth()
    {
        CanFollowPlayer = false;
        Camera.transform.DOMove(LaberinthTarget.position, 3).OnComplete(ChangeCameraLaberinth_Callback);
        Camera.transform.DORotate(LaberinthTarget.eulerAngles, 3);
        PlayerManagerControllers.instance.LockPlayerControl();
    }

    private void ChangeCameraLaberinth_Callback()
    {
        CalculateOffset();
        CanFollowPlayer = true;
        PlayerManagerControllers.instance.LockPlayerControl();
        
    }

    private void FollowPlayer()
    {
        if(CanFollowPlayer){
            Camera.transform.position = Player.position - offset;

        }
        else
        {
            
        }
    }

    public void CalculateOffset()
    {
        offset = Player.position - Camera.transform.position;
    }


}

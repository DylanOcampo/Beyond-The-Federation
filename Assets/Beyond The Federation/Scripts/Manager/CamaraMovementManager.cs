using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

    public Transform Player, LaberinthTarget, NormalPositionTarget, FinishLaberinthTarget;

    public GameObject CameraParent, ActualCamera;
    public PostProcessVolume postv;

    private Vector3 offset;

    public bool CanFollowPlayer;

    public bool HasEnteredLAB;


    // Start is called before the first frame update
    void Start()
    {
        CalculateOffset();
        
        
            try
            {
                PostProcessingManager.instance.ChangeConfiginGameScene(ActualCamera, postv);
            }
            catch (System.NullReferenceException)
            {
                //Debug.Log("Start in Main Menu");
            }

            
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    public void ChangeCameraLaberinth()
    {
        CanFollowPlayer = false;

        

        CameraParent.transform.DOMove(LaberinthTarget.position, 3).OnComplete(ChangeCameraLaberinth_Callback);

        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = .02f;

        AudioManager.instance.PlayClipMusic(2);

        CameraParent.transform.DORotate(LaberinthTarget.eulerAngles, 3);

        PlayerManagerControllers.instance.LockPlayerControl();
        HasEnteredLAB = true;
    }

    public void ChangeCameraNormal(bool AmIExit)
    {
        if (HasEnteredLAB)
        {
            CanFollowPlayer = false;

            if (!AmIExit)
            {
                CameraParent.transform.DOMove(NormalPositionTarget.position, 3).OnComplete(ChangeCameraNormal_Callback);
            }
            else
            {
                CameraParent.transform.DOMove(FinishLaberinthTarget.position, 3).OnComplete(ChangeCameraNormal_Callback);
            }

            

            RenderSettings.fogColor = Color.white;

            RenderSettings.fogDensity = 0.002f;

            AudioManager.instance.PlayClipMusic(1);

            CameraParent.transform.DORotate(new Vector3(5, 180, 0), 3);

            PlayerManagerControllers.instance.LockPlayerControl();
            HasEnteredLAB = false;
        }
        
    }

    private void ChangeCameraNormal_Callback()
    {
        CalculateOffset();
        CanFollowPlayer = true;
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
            CameraParent.transform.position = Player.position - offset;

        }
        
    }

    public void CalculateOffset()
    {
        offset = Player.position - CameraParent.transform.position;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

public class PostProcessingManager : MonoBehaviour
{
    private static PostProcessingManager _instance;

    public static PostProcessingManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PostProcessingManager>();
            }
            return _instance;
        }
    }

    public bool CameraMotionBlur, Antialiasing, AmbientOcclusio;

    public int qualitySettings;
    private AmbientOcclusion ambient;

    public void LowQ()
    {
        qualitySettings = 0;
        
    }

    public void MediumQ()
    {
        qualitySettings = 3;
        
    }

    public void HighQ()
    {
        qualitySettings = 6;
        
    }


    public void ChangeConfiginGameScene(GameObject Camera, PostProcessVolume postv)
    {
        QualitySettings.SetQualityLevel(qualitySettings, true);
        Camera.GetComponentInChildren<CameraMotionBlur>().enabled = CameraMotionBlur;
        Camera.GetComponentInChildren<Antialiasing>().enabled = Antialiasing;
        postv.profile.TryGetSettings(out ambient);
        ambient.enabled.value = AmbientOcclusio;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



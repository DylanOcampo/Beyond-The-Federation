using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PostProcessingInterface : MonoBehaviour
{

    public TextMeshProUGUI Desenfoque, Anti, Ambient;

    private bool boolDesenfoque, boolAnti, boolAmbient;


    public int qualitySettings;

    private void Start()
    {
        if (PostProcessingManager.instance.CameraMotionBlur)
        {
            Desenfoque.text = "ON";
            boolDesenfoque = true;
        }
        else
        {
            Desenfoque.text = "OFF";
            boolDesenfoque = false;
        }

        if (PostProcessingManager.instance.Antialiasing)
        {
            Anti.text = "ON";
            boolAnti = true;
        }
        else
        {
            Anti.text = "OFF";
            boolAnti = false;
        }

        if (PostProcessingManager.instance.AmbientOcclusio)
        {
            Ambient.text = "ON";
            boolAmbient = true;
        }
        else
        {
            Ambient.text = "OFF";
            boolAmbient = false;
        }



    }

    public void Quality(int _quality)
    {
        if(_quality == 0)
        {
            PostProcessingManager.instance.HighQ();
            
        }
        if(_quality == 1)
        {
            PostProcessingManager.instance.MediumQ();
        }
        if(_quality == 2)
        {
            PostProcessingManager.instance.LowQ();
        }
    }



    public void ChangeMotionBlur()
    {
        boolDesenfoque = !boolDesenfoque;
        if (boolDesenfoque)
        {
            Desenfoque.text = "ON";
            PostProcessingManager.instance.CameraMotionBlur = true;
        }
        else
        {
            Desenfoque.text = "OFF";
            PostProcessingManager.instance.CameraMotionBlur = false;
        }
    }

    public void ChangeAntialiasing()
    {
        boolAnti = !boolAnti;
        if (boolAnti)
        {
            Anti.text = "ON";
            PostProcessingManager.instance.Antialiasing = true;
        }
        else
        {
            Anti.text = "OFF";
            PostProcessingManager.instance.Antialiasing = false;
        }
    }

    public void ChangeAmbientOcclusion()
    {
        boolAmbient = !boolAmbient;
        if (boolAmbient)
        {
            Ambient.text = "ON";
            PostProcessingManager.instance.AmbientOcclusio = true;
        }
        else
        {
            Ambient.text = "OFF";
            PostProcessingManager.instance.AmbientOcclusio = false;
        }
    }

}

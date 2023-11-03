using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class ThirdEyeSystem : MonoBehaviour
{
    private static ThirdEyeSystem _instance;
    public static ThirdEyeSystem instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ThirdEyeSystem>();
            }
            return _instance;
        }
    }

    public class ThirdEyeSystemEvent : UnityEvent<bool> { }
    
    public ThirdEyeSystemEvent OnActivate;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public event Action onThirdEyeSystemEnter;
    public void ThirdEyeSystemEnter()
    {
        Debug.Log("a");
        if(onThirdEyeSystemEnter != null)
        {
            onThirdEyeSystemEnter();
        }
    }

    public event Action onThirdEyeSystemExit;
    public void ThirdEyeSystemExit()
    {
        if (onThirdEyeSystemExit != null)
        {
            onThirdEyeSystemExit();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;

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
    public GameObject  CanvasColor;
    public Camera MainCamera;
    private bool Switch = false;
    private int NormalViewMask;


    // Start is called before the first frame update
    void Start()
    {
        NormalViewMask = MainCamera.cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(2)){
            Switch = !Switch;
            if(Switch){
                CanvasColor.SetActive(true);
                MainCamera.cullingMask = -1;
                CanvasColor.GetComponent<CanvasGroup>().DOFade(1, .5f);
                
            }else{
                MainCamera.cullingMask = NormalViewMask;
                
                CanvasColor.GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() => {CanvasColor.SetActive(false);});
            }
            OnActivate?.Invoke(Switch);
        }
        
    }
    private void ChangeCameras(bool _value){
        
        
    }

}

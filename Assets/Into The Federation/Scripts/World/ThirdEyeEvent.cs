using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEyeEvent : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        ThirdEyeSystem.instance.onThirdEyeSystemEnter += ThirdEyeSystemEnter;
        ThirdEyeSystem.instance.onThirdEyeSystemEnter += ThirdEyeSystemExit;

    }
    void OnDestroy(){
        //ThirdEye.OnActivate.RemoveListener(OnActivate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThirdEyeSystemEnter(){
        gameObject.SetActive(true);
    }

    public void ThirdEyeSystemExit()
    {
        gameObject.SetActive(false);
    }
}

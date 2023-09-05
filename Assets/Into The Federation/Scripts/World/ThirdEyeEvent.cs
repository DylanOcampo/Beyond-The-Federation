using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ThirdEyeEvent : MonoBehaviour
{
    public ThirdEyeSystem ThirdEye;
    // Start is called before the first frame update
    void Start()
    {
        ThirdEye.OnActivate.AddListener(OnActivate);
    }
    void OnDestroy(){
        ThirdEye.OnActivate.RemoveListener(OnActivate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActivate(bool _value){
        gameObject.SetActive(_value);
    }
}

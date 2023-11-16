using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEyeEvent : MonoBehaviour
{
    public enum TypeOfEvent {ShowObject, Collider, Interactable, ParticleSystem}
    public TypeOfEvent types;


    // Start is called before the first frame update
    void Start()
    {
        ThirdEyeSystem.instance.onThirdEyeSystemEnter += ThirdEyeSystemEnter;
        ThirdEyeSystem.instance.onThirdEyeSystemExit += ThirdEyeSystemExit;
        if (types == TypeOfEvent.ParticleSystem)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }
    }




    void OnDestroy(){
        //ThirdEye.OnActivate.RemoveListener(OnActivate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThirdEyeSystemEnter(){
        
        if (types == TypeOfEvent.ShowObject)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        if(types == TypeOfEvent.Collider)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if(types == TypeOfEvent.ParticleSystem)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }

        if( types == TypeOfEvent.Interactable)
        {

        }


        
    }

    public void ThirdEyeSystemExit()
    {
        if (types == TypeOfEvent.ShowObject)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (types == TypeOfEvent.Collider)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        if (types == TypeOfEvent.Interactable)
        {

        }
        if (types == TypeOfEvent.ParticleSystem)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }

    }
}

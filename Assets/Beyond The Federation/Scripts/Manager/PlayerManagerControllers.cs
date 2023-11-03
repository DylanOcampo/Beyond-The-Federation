using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManagerControllers : MonoBehaviour
{
    public GameObject Cellbit, Roier, Camara;

    public KeyCode ChangeCharacterKey;

    bool SwitchCharacter = false;

    bool LockPlayer = false;

    private static PlayerManagerControllers _instance;
    public static PlayerManagerControllers instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManagerControllers>();
            }
            return _instance;
        }
    }


    public void AnimationsCompanion(bool? _flipX = null, bool? _movingBackwards = null)
    {
        
        if (!SwitchCharacter)
        {
            
            Cellbit.GetComponent<CompanionAI>().FlipAnimate(_flipX, _movingBackwards);
        }
        else
        {
            Roier.GetComponent<CompanionAI>().FlipAnimate(_flipX, _movingBackwards);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ChangeCharacterKey))
        {
            ChangePlayableCharacter();
        }
    }

    public void LockPlayerControl()
    {
        if (!LockPlayer)
        {
            Roier.GetComponent<RoierPlayer>().enabled = false;
            Cellbit.GetComponent<CellbitPlayer>().enabled = false;
        }
        else
        {
            if (!SwitchCharacter)
            {
                Roier.GetComponent<RoierPlayer>().enabled = true;
            }
            else
            {
                Cellbit.GetComponent<CellbitPlayer>().enabled = true;
            }
        }

        LockPlayer = !LockPlayer;
    }



    private void ChangePlayableCharacter()
    {
        if(SwitchCharacter)
        {
            //Change to Roier
            Cellbit.GetComponent<CompanionAI>().enabled = true;
            Cellbit.GetComponent<NavMeshAgent>().enabled = true;

            Roier.layer = LayerMask.NameToLayer("Player");
            Roier.tag = "Player";
            Cellbit.layer = LayerMask.NameToLayer("Default");
            Cellbit.tag = "Untagged";
            Roier.GetComponent<CompanionAI>().enabled = false;
            Roier.GetComponent<NavMeshAgent>().enabled = false;

            CamaraMovementManager.instance.Player = Roier.transform;
            Camara.GetComponentInChildren<FadeObjectBlockingObject>().Player = Roier.transform;


            Roier.GetComponent <RoierPlayer>().enabled = true;
            Cellbit.GetComponent<CellbitPlayer>().enabled = false;
        }
        else
        {
            //Change to cellbit
            Cellbit.GetComponent<CompanionAI>().enabled = false;
            Cellbit.GetComponent<NavMeshAgent>().enabled = false;

            Cellbit.layer = LayerMask.NameToLayer("Player");
            Cellbit.tag = "Player";
            Roier.layer = LayerMask.NameToLayer("Default");
            Roier.tag = "Untagged";
            Roier.GetComponent<CompanionAI>().enabled = true;
            Roier.GetComponent<NavMeshAgent>().enabled = true;
            CamaraMovementManager.instance.Player = Cellbit.transform;
            Camara.GetComponentInChildren<FadeObjectBlockingObject>().Player = Cellbit.transform;

            Roier.GetComponent<RoierPlayer>().enabled = false;
            Cellbit.GetComponent<CellbitPlayer>().enabled = true;
        }
        SwitchCharacter = !SwitchCharacter;
    }


}

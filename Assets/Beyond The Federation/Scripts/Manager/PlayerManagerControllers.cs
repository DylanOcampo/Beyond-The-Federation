using DG.Tweening;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.Wrappers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManagerControllers : MonoBehaviour
{
    public GameObject Cellbit, Roier, Camara;

    public KeyCode ChangeCharacterKey;

    bool SwitchCharacter = false;

    public bool LockPlayer = false;

   

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
            Debug.Log(SwitchCharacter);
        }
   
    }

    public void RotatePlayers(Vector3 value)
    {
        Cellbit.transform.DORotate(value, 3).OnComplete(() =>
        {
            if (value.y == 0)
            {
                
                Cellbit.GetComponent<CompanionAI>().CameraLaberynth = true;
            }
        });
        Roier.transform.DORotate(value, 3).OnComplete(() =>
        {
            if (value.y == 0)
            {

                Roier.GetComponent<CompanionAI>().CameraLaberynth = true;
            }
        });
        
    }


    public void LockPlayerControl()
    {
        if (!LockPlayer)
        {
            Roier.GetComponent<RoierPlayer>().enabled = false;
            Cellbit.GetComponent<CellbitPlayer>().enabled = false;
            Cellbit.GetComponent<CompanionAI>().enabled = false;
            Roier.GetComponent<CompanionAI>().enabled = false;
        }
        else
        {
            if (!SwitchCharacter)
            {
                Roier.GetComponent<RoierPlayer>().enabled = true;
                Cellbit.GetComponent<CompanionAI>().enabled = true;
            }
            else
            {
                Cellbit.GetComponent<CellbitPlayer>().enabled = true;
                Roier.GetComponent<CompanionAI>().enabled = true;
            }
        }

        LockPlayer = !LockPlayer;
    }

    public Vector3 GetSpace()
    {
        if (!SwitchCharacter)
        {
            if (Roier.GetComponent<RoierPlayer>().PlayerSpriteRenderer.flipX)
            {
                return Cellbit.transform.position = Roier.GetComponent<RoierPlayer>().LeftRespawn.transform.position;
            }
            else
            {
                return Cellbit.transform.position = Roier.GetComponent<RoierPlayer>().RightRespawn.transform.position;

            }
            
        }
        else
        {
            if (Cellbit.GetComponent<CellbitPlayer>().PlayerSpriteRenderer.flipX)
            {
                return Roier.transform.position = Cellbit.GetComponent<CellbitPlayer>().LeftRespawn.transform.position;
            }
            else
            {
                return Roier.transform.position = Cellbit.GetComponent<CellbitPlayer>().RightRespawn.transform.position;

            }
            
        }
    }


    public void RespawnCompanion()
    {
        /*
        Debug.Log(IsPlayerOnGround());
        if (IsPlayerOnGround())
        {
            
            if (!SwitchCharacter)
            {
                if (Roier.GetComponent<RoierPlayer>().PlayerSpriteRenderer.flipX)
                {
                    Cellbit.transform.position = Roier.GetComponent<RoierPlayer>().LeftRespawn.transform.position;
                }
                else
                {
                    Cellbit.transform.position = Roier.GetComponent<RoierPlayer>().RightRespawn.transform.position;

                }
                Cellbit.GetComponent <CompanionAI>().playerInFollowRange = true;
            }
            else
            {
                if (Cellbit.GetComponent<CellbitPlayer>().PlayerSpriteRenderer.flipX)
                {
                    Roier.transform.position = Cellbit.GetComponent<CellbitPlayer>().LeftRespawn.transform.position;
                }
                else
                {
                    Roier.transform.position = Cellbit.GetComponent<CellbitPlayer>().RightRespawn.transform.position;

                }
                Roier.GetComponent<CompanionAI>().playerInFollowRange = true;
            }
        }
        */
        
    }
    public bool IsPlayerOnGround()
    {
        if (!SwitchCharacter)
        {
            return !Roier.GetComponent<RoierPlayer>().PlayerAnimation.GetBool("Air");
        }
        else
        {
            return !Cellbit.GetComponent<CellbitPlayer>().PlayerAnimation.GetBool("Air");
        }

        
    }



    private void ChangePlayableCharacter()
    {
        if (SwitchCharacter)
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


            Roier.GetComponent<RoierPlayer>().enabled = true;
            Cellbit.GetComponent<CellbitPlayer>().enabled = false;


            Roier.GetComponent<BoxCollider>().enabled = true;


            Cellbit.GetComponent<BoxCollider>().enabled = false;

            DialogueLua.SetVariable("SoyRoier", true);


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


            Roier.GetComponent<BoxCollider>().enabled = false;

            Cellbit.GetComponent<BoxCollider>().enabled = true;

            DialogueLua.SetVariable("SoyRoier", false);
        }
        SwitchCharacter = !SwitchCharacter;
    }


}

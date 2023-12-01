using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionAI : MonoBehaviour
{
    public NavMeshAgent agentPlayer;

    public float followRange, standRange;
    public LayerMask whatIsPlayer;
    public bool playerInFollowRange, playerInStandRange;
    public GameObject player, camara;
    public SpriteRenderer PlayerSpriteRenderer;
    public Animator FlipAnimation, animator;

    public bool CameraLaberynth = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInFollowRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);
        playerInStandRange = Physics.CheckSphere(transform.position, standRange, whatIsPlayer);

        if(playerInFollowRange)
        {
            if (!playerInStandRange)
            {
                ChasePlayer();
            }
            else
            {
                agentPlayer.SetDestination(gameObject.transform.position);
                animator.SetFloat("moveSpeed", 0);
            }

        }
        else
        {
            PlayerManagerControllers.instance.RespawnCompanion();
        }


        gameObject.transform.eulerAngles = Vector3.zero;
        
        
    }

    public void WaitforJump()
    {

    }


    public void FlipAnimate(bool? _flipX, bool? _movingBackwards )
    {
        
        if (_flipX != null)
        {
            PlayerSpriteRenderer.flipX = (bool)_flipX;
            FlipAnimation.SetTrigger("Flip");
        }else if(_movingBackwards != null)
        {
            //MovingBackwards = _movingBackwards;
            FlipAnimation.SetTrigger("Flip");
        }
        
    }


    private void ChasePlayer()
    {
        agentPlayer.SetDestination(player.transform.position);
        if(agentPlayer.velocity.magnitude > 0 )
        {
            animator.SetFloat("moveSpeed", 1);
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, standRange);
    }


}

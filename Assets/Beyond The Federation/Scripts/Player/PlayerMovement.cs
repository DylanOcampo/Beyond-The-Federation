using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody PlayerRigidBody;
    public float MoveSpeed, JumpForce;
    public Animator PlayerAnimation, FlipAnimation;
    public SpriteRenderer PlayerSpriteRenderer;
    public LayerMask WhatIsGround;
    public Transform GroundPoint;
    
    private bool IsGrounded, MovingBackwards;
    private Vector2 MoveInput;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput.x = Input.GetAxis("Horizontal");
        MoveInput.y = Input.GetAxis("Vertical");
        MoveInput.Normalize();

        PlayerRigidBody.velocity = new Vector3(MoveInput.x * MoveSpeed, PlayerRigidBody.velocity.y, MoveInput.y *MoveSpeed);
        PlayerAnimation.SetFloat("moveSpeed", PlayerRigidBody.velocity.magnitude);

        RaycastHit hit;
        if(Physics.Raycast(GroundPoint.position, Vector3.down, out hit, .3f, WhatIsGround)){
            IsGrounded = true;
        }else{
            IsGrounded = false; 
        }


        if(Input.GetButtonDown("Jump") && IsGrounded){
            PlayerRigidBody.velocity += new Vector3(0f, JumpForce, 0f);
        }

        PlayerAnimation.SetBool("onGround", IsGrounded);

        if(!PlayerSpriteRenderer.flipX && MoveInput.x < 0){
            PlayerSpriteRenderer.flipX = true;
            FlipAnimation.SetTrigger("Flip");

        }else if(PlayerSpriteRenderer.flipX && MoveInput.x > 0){
            PlayerSpriteRenderer.flipX = false;
            FlipAnimation.SetTrigger("Flip");
        }


        if(!MovingBackwards && MoveInput.y > 0){
            MovingBackwards = true;
            FlipAnimation.SetTrigger("Flip");
        }else if(MovingBackwards && MoveInput.y < 0 ){
            MovingBackwards = false;
            FlipAnimation.SetTrigger("Flip");
        }
        PlayerAnimation.SetBool("movingBackwards", MovingBackwards);



    }


}

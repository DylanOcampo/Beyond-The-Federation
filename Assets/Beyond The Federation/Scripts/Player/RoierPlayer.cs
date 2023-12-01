using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RoierPlayer : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float airMinSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Attack")]
    public bool isPushing = false;
    public float ForceRecoil = 10;

    [Header("Animations")]
    public Animator PlayerAnimation, FlipAnimation;
    public SpriteRenderer PlayerSpriteRenderer;
    private bool MovingBackwards;


    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float fallMultiplier;
    public float airMultiplier;
    bool doublejump = false;
    bool readyToJump = false;
    bool DoOnceJump = true;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode pushkey = KeyCode.Mouse1;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("References")]
    public Transform orientation;
    public GameObject PauseMenu, Light, LeftRespawn,  RightRespawn;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallrunning,
        climbing,
        vaulting,
        crouching,
        sliding,
        air
    }

    public bool crouching, freeze, unlimited;



    [HideInInspector]
    public bool CanAttack = true;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        PlayerAnimation.SetBool("onGround", grounded);

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (OnSlope())
            if (Round(rb.velocity.magnitude, 1) > 1)
            {
                PlayerAnimation.SetFloat("moveSpeed", Round(rb.velocity.magnitude, 1));
                try
                {
                    AudioManager.instance.StopFootSteps();
                }
                catch (NullReferenceException)
                {
                    Debug.Log("Start From Main Menu");
                }
            }
            else
            {
                PlayerAnimation.SetFloat("moveSpeed", 0);
                try
                {
                    AudioManager.instance.StopFootSteps();
                }
                catch (NullReferenceException)
                {
                    Debug.Log("Start From Main Menu");
                }
            }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else
        {
            if (Round(flatVel.magnitude, 1) > 1)
            {
                PlayerAnimation.SetFloat("moveSpeed", Round(flatVel.magnitude, 1));
                try
                {
                    AudioManager.instance.StopFootSteps();
                }
                catch (NullReferenceException)
                {
                    Debug.Log("Start From Main Menu");
                }

            }
            else
            {
                PlayerAnimation.SetFloat("moveSpeed", 0);
                try
                {
                    AudioManager.instance.StopFootSteps();
                }
                catch (NullReferenceException)
                {
                    //Debug.Log("Start From Main Menu");
                }
                
            }
        }

        PlayerAnimation.SetBool("movingBackwards", MovingBackwards);

        if (state == MovementState.air)
        {
            PlayerAnimation.SetBool("Air", true);
        }
        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching || grounded)
        {
            PlayerAnimation.SetBool("Air", false);
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // when to jump

        if (Input.GetKey(jumpKey))
        {

            if (grounded || doublejump)
            {
                if (readyToJump)
                {
                    Jump();
                    readyToJump = false;

                }
                if (doublejump)
                {
                    
                    doublejump = false;
                    
                }

            }

        }
        if (Input.GetKeyDown(pushkey))
        {
            Debug.Log("Pusheando");
            isPushing = true;

        }
        if (Input.GetKeyUp(pushkey))
        {

            isPushing = false;

        }

        if (Input.GetKeyUp(jumpKey) && DoOnceJump)
        {

            doublejump = true;
            DoOnceJump = false;

        }

        if (grounded && !Input.GetKey(jumpKey))
        {

            doublejump = false;
            DoOnceJump = true;
            if (!readyToJump)
            {
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        if (Input.GetKey(attackKey) && grounded && CanAttack && !PauseMenu.activeSelf)
        {
            
            Attack();
            CanAttack = false;
        }




        // start crouch
        if (Input.GetKeyDown(crouchKey) && horizontalInput == 0 && verticalInput == 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            crouching = true;
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

            crouching = false;
        }

        if (!PlayerSpriteRenderer.flipX && horizontalInput > 0)
        {
            PlayerSpriteRenderer.flipX = true;
            FlipAnimation.SetTrigger("Flip");
            PlayerManagerControllers.instance.AnimationsCompanion(true);

        }
        else if (PlayerSpriteRenderer.flipX && horizontalInput < 0)
        {
            PlayerSpriteRenderer.flipX = false;
            FlipAnimation.SetTrigger("Flip");
            PlayerManagerControllers.instance.AnimationsCompanion(false);
        }

        if (!MovingBackwards && verticalInput < 0)
        {
            MovingBackwards = true;
            FlipAnimation.SetTrigger("Flip");
            PlayerManagerControllers.instance.AnimationsCompanion(null, true);
        }
        else if (MovingBackwards && verticalInput > 0)
        {
            MovingBackwards = false;
            FlipAnimation.SetTrigger("Flip");
            PlayerManagerControllers.instance.AnimationsCompanion(null, false);
        }
    }

    bool keepMomentum;
    private void StateHandler()
    {
        // Mode - Freeze
        if (freeze)
        {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
            desiredMoveSpeed = 0f;
        }

        // Mode - Unlimited
        else if (unlimited)
        {
            state = MovementState.unlimited;
            desiredMoveSpeed = 999f;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;

            if (moveSpeed < airMinSpeed)
                desiredMoveSpeed = airMinSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;

        // deactivate keepMomentum
        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        
        

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);


            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    private void Attack()
    {
        PlayerAnimation.SetTrigger("Attack");
        AudioManager.instance.PlayClip(2);


    }

    public void RecoilAttack() {
        if (PlayerSpriteRenderer.flipX)
        {
            rb.AddForce(-gameObject.transform.right * ForceRecoil, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(gameObject.transform.right * ForceRecoil, ForceMode.Impulse);
        }
    }


    private void Jump()
    {
        Debug.Log("jump");
        exitingSlope = true;
        try
        {
            AudioManager.instance.PlayClip(20);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Start From Main Menu");
        }
        
        
        if(rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        PlayerAnimation.SetTrigger("Jumping");
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }


    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

}


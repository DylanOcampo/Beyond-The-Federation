using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CellbitPlayer : MonoBehaviour
{

    [Header("Movement")]
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    public float walkSpeed;
    public float airMinSpeed;


    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Attack")]

    [Header("Animations")]
    public Animator PlayerAnimation, FlipAnimation;
    public SpriteRenderer PlayerSpriteRenderer;
    private bool MovingBackwards;


    [Header("Jumping")]
    public float jumpForce;
    public float fallMultiplier;
    public float jumpCooldown;
    public float airMultiplier;

    bool HasLight = false;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode ThirdEyeInput = KeyCode.Mouse2;
    public KeyCode Feather = KeyCode.Mouse0;
    public KeyCode Light = KeyCode.Q;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Feather")]
    public GameObject FeatherSpawnRight;
    public GameObject FeatherSpawnLeft;
    public GameObject PrefabFeather;
    public float FeatherForce;

    [Header("References")]
    public GameObject PauseMenu, LeftRespawn, RightRespawn;

    public Transform orientation;

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
    public bool crouching;
    

    public bool freeze;
    public bool unlimited;

    public bool restricted;


    [Header("ThirdEye")]
    
    public GameObject CanvasColor;
    public Camera MainCamera;
    private bool Switch = false;
    private int NormalViewMask;



    [HideInInspector]
    public bool CanAttack = true;

    private void Start()
    {
        NormalViewMask = MainCamera.cullingMask;
         
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        

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

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (OnSlope())
            if (Round(rb.velocity.magnitude, 1) > 1)
            {
                PlayerAnimation.SetFloat("moveSpeed", Round(rb.velocity.magnitude, 1));
                AudioManager.instance.PlayFootSteps();
            }
            else
            {
                PlayerAnimation.SetFloat("moveSpeed", 0);
                AudioManager.instance.StopFootSteps();
            }

        else
        {
            if (Round(flatVel.magnitude, 1) > 1)
            {
                PlayerAnimation.SetFloat("moveSpeed", Round(flatVel.magnitude, 1));
                AudioManager.instance.PlayFootSteps();
            }
            else
            {
                PlayerAnimation.SetFloat("moveSpeed", 0);
                //AudioManager.instance.StopFootSteps();
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


    private void ThirdEye()
    {
        Switch = !Switch;
        if (Switch)
        {
            
            CanvasColor.SetActive(true);
            MainCamera.cullingMask = -1;
            CanvasColor.GetComponent<CanvasGroup>().DOFade(1, .5f);
            ThirdEyeSystem.instance.ThirdEyeSystemEnter();
            AudioManager.instance.PlayClip(27);
        }
        else
        {
            MainCamera.cullingMask = NormalViewMask;

            CanvasColor.GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() => { CanvasColor.SetActive(false); ThirdEyeSystem.instance.ThirdEyeSystemExit(); });
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpawnFeather()
    {
        AudioManager.instance.PlayClip(4);
        GameObject Instance = Instantiate(PrefabFeather);
        if (!PlayerSpriteRenderer.flipX) {
            Instance.transform.position = FeatherSpawnRight.transform.position;
            Instance.GetComponent<Rigidbody>().AddForce(FeatherSpawnRight.transform.forward * FeatherForce, ForceMode.Impulse);
            Instance.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            Instance.transform.position = FeatherSpawnLeft.transform.position;
            Instance.GetComponent<Rigidbody>().AddForce(FeatherSpawnLeft.transform.forward * FeatherForce, ForceMode.Impulse);
        }
        
        
        StartCoroutine(DestroyObject(Instance));
    }

    IEnumerator DestroyObject(GameObject GA)
    {
        yield return new WaitForSeconds(3);
        Destroy(GA);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        horizontalInput = -horizontalInput;
        verticalInput = -verticalInput;

        // when to jump

        if (Input.GetKeyDown(ThirdEyeInput))
        {
            ThirdEye();

        }

        if (Input.GetKeyDown(Feather) && !PauseMenu.activeSelf)
        {
            SpawnFeather();
        }

        if (Input.GetKeyDown(jumpKey))
        {

            if (grounded )
            {
                Jump();
                

            }

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
            PlayerManagerControllers.instance.AnimationsCompanion( true);

        }
        else if (PlayerSpriteRenderer.flipX && horizontalInput < 0)
        {
            PlayerSpriteRenderer.flipX = false;
            FlipAnimation.SetTrigger("Flip");
            PlayerManagerControllers.instance.AnimationsCompanion( false);
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
        //if (climbingScript.exitingWall) return;
        //if (climbingScriptDone.exitingWall) return;
        if (restricted) return;

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

    }

    private void Jump()
    {
        exitingSlope = true;
        AudioManager.instance.PlayClip(20);
        // reset y velocity

        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        PlayerAnimation.SetTrigger("Jumping");
    }
    private void ResetJump()
    {
        

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




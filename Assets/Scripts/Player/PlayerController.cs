using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    //COMPONENTS
    [Header("COMPONENTS")]
    Transform trans;
    Rigidbody rb;
    Animator anim;
    Player_Stats playerStats;
    GameInputs input;
    HandleNearEnemies nearEnemies;

    //PHYSICS
    [Header("PHYSICS")]
    CapsuleCollider capsule;
    float capsuleHeight;
    Vector3 capsuleCenter;

    public bool isGrounded;
    public float groundCheckDistance = 0.1f;
    float origGroundCheckDistance;
    Vector3 groundNormal;


    //MOVEMENT
    [Header("MOVEMENT")]
    public float movingTurnSpeed = 360;
    float stationaryTurnSpeed = 180;
    public float moveSpeedMultiplier = 2;
    public float animSpeedMultiplier = 1f;
    Vector3 move;
    float speed;
    public float runCost;

    float turnAmount;
    float forwardAmount;

    Vector3 vec;

    public bool canMove;
    
    [Header("INPUTS")]
    float inputCounter;

    [Header("MELEE ATTACK")]
    public bool isMeleeAttack;
    public float meleeDamage;
    BoxCollider meleeCollider;
    MeleeCollider meleeCollScript;
    public float stopAttackMelee;
    public float meleeCost;

    [Header("RUN")]
    public float runTime;
    public bool isRunning;

    [Header("DODGE")]
    public float dodgeTime;
    public bool isDodge;
    public float dodgeCost;
    
    
    [Header("CAMERA")]
    Transform cam;                  
    Vector3 camForward;
    Vector3 camSide;
    CursorLockMode cursor;

    [Header("TARGETING")]
    public bool lockTarget = false;
    float sidewayAmount;
    Vector3 directionPos;
    Vector3 storeDir;
    public float turnSpeed = 5f;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerStats = GetComponent<Player_Stats>();
        input = GetComponent<GameInputs>();
        nearEnemies = GetComponent<HandleNearEnemies>();

        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        origGroundCheckDistance = groundCheckDistance;

        cam = Camera.main.transform;

        isGrounded = true;

        cursor = CursorLockMode.Locked;

        GameObject obj = GameObject.FindGameObjectWithTag("PlayerMelee");
        meleeCollider = obj.GetComponent<BoxCollider>();
        meleeCollScript = obj.GetComponent<MeleeCollider>();

        isRunning = false;
    }

    private void FixedUpdate()
    {
        ReadInputs();
        
        if (!lockTarget)
        {
            HandleNormalMovement();
        }
        else
        {

            HandleLockOnMovement();
        }
        
    }


    void ReadInputs()
    {
        
        if (input.meleeAttackInput && !isMeleeAttack && isGrounded && playerStats.HasStamina(meleeCost))
        {
            isMeleeAttack = true;
            playerStats.RecieveStamina(meleeCost);
        }

        if (isMeleeAttack)
        {
            meleeCollScript.SetDamage(meleeDamage);
        }
        else
        {
            meleeCollScript.SetDamage(0);
        }

        if (input.runInput && playerStats.HasStamina(runCost + 5))
        {
            isRunning = true;
        }
        else isRunning = false;

        if (input.dodgeInput && !isDodge && isGrounded && !isMeleeAttack)
        {
            if (playerStats.HasStamina(dodgeCost))
            {
                isDodge = true;
                playerStats.RecieveStamina(dodgeCost);
                anim.SetTrigger("Dodge");
            }
        }

        if (input.camModeInput) lockTarget = !lockTarget;

        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        camSide = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;
        move = input.vertical * camForward + input.horizontal * cam.right;
        
    }


    //MOVEMENT
    void HandleNormalMovement()
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z);

        if (!isDodge)
        {
            forwardAmount = move.z;
        }
        else
        {
            forwardAmount -= Time.deltaTime * 5;
        }
            
        if(isRunning)
        {
            forwardAmount += 1;
            playerStats.RecieveStamina(runCost);
            
        } 
       
        if(!anim.GetBool("isMeleeAttack"))ApplyExtraTurnRotation();
        
        if (isGrounded && Time.deltaTime > 0)
        {
            vec = (anim.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
            vec.y = rb.velocity.y;
            rb.velocity = vec;
        }
        if (!isDodge || !isMeleeAttack) anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);

        UpdateAnimator();
    }

    void HandleLockOnMovement()
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z);

        if (!isDodge)
        {
            forwardAmount = move.z;
            sidewayAmount = move.x;

            Vector3 lookPos = nearEnemies.target;

            Vector3 lookDir = lookPos - transform.position;

            lookDir.y = 0;

            Quaternion rot = Quaternion.LookRotation(lookDir);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
        else
        {
            forwardAmount -= Time.deltaTime * 5;
            sidewayAmount -= Time.deltaTime * 5;

            ApplyExtraTurnRotation();
        }
        Vector3 moveForward = input.vertical * camForward;
        Vector3 moveSideways = input.horizontal * cam.right;

        

        if (isGrounded && Time.deltaTime > 0)
        {
            vec = (anim.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
            vec.y = rb.velocity.y;
            rb.velocity = vec;
        }
        if (!isDodge || !isMeleeAttack)
        {
            anim.SetFloat("Forward", forwardAmount, 0.2f, Time.deltaTime);
            anim.SetFloat("Sideways", sidewayAmount *2, 0.2f, Time.deltaTime);
        }

        

        UpdateAnimator();
    }

    void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }
    

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

         if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
            anim.applyRootMotion = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            anim.applyRootMotion = false;
        }
    }

   


    //ANIMATION FUNCTIONS_______________________________________________________________________
    void UpdateAnimator()
    {
        // update the animator parameters

        anim.SetBool("LockOn", lockTarget);

        anim.SetBool("OnGround", isGrounded);

        anim.SetBool("isMeleeAttack", isMeleeAttack);
        
        if (isGrounded && move.magnitude > 0)
        {
            anim.speed = animSpeedMultiplier;
        }
        else
        {
            anim.speed = 1;
        }
    }

    
}

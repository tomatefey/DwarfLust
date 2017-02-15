using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
/*
    [Header("Components")]
    public Rigidbody rb;
    public Animator anim;
    public CapsuleCollider cap;
    public Transform cam;
    public Transform camHolder;         //cam follows camHolder, so cam can have a smooth movement
    public GameInputs input;            //Read inputs

    [Header("Movement")]
    public float lockSpeed = 0.5f;
    public float normSpeed = 0.8f;
    float speed;
    public float turnSpeed = 5;
    
    public Vector3 directionPos;
    public Vector3 storeDir;

    [Header("Targeting")]
    public bool lockTarget;
    public int curTarget;
    public bool changeTarget;

    public float targetTurnAmount;
    public float curTurnAmount;
    public bool canMove;
    public List<Transform> Enemies = new List<Transform>();

    public Transform camTarget;
    public float camTargetSpeed = 5;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        camHolder = cam.parent.parent;
        cap = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        input = GetComponent<GameInputs>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        UpdateDirection();

        if (canMove)
        {
            if (!lockTarget)
            {
                speed = normSpeed;
                HandleMovementNormal();
            }
            else
            {
                speed = lockSpeed;

                if(Enemies.Count > 0)
                {
                    HandleMovementLockOn();
                    HandleRotationOnLock();
                }
                else
                {
                    lockTarget = false;
                }
            }
        }
	}

    void UpdateDirection()
    {
        storeDir = camHolder.right;

        ChangeTargetsLogic();
    }

    void ChangeTargetsLogic()
    {
        if (input.camModeInput)
        {
            lockTarget = !lockTarget;
        }

        if (input.changeTargetInput)
        {
            if(curTarget < Enemies.Count - 1)
            {
                curTarget++;
            }
            else
            {
                curTarget = 0;
            }
        }
    }

    void HandleCameraTarget()
    {
        if (!lockTarget)
        {
            targetPos = transform.position;
        }
        else
        {
            if(Enemies.Count > 0)
            {
                Vector3 direction = Enemies[curTarget].position - transform.position;
                direction.y = 0;

                float distance = Vector3.Distance(transform.position, Enemies[curTarget].position);

                targetPos = direction.normalized * distance / 4;
                targetPos += transform.position;

                if(distance > 10)
                {
                    lockTarget = false;
                }
            }
        }
    }

    void HandleMovementNormal()
    {
        canMove = anim.GetBool("CanMove");

        Vector3 dirForward = storeDir * input.horizontal;
        Vector3 dirSides = camHolder.forward * input.vertical;

        Vector3 move = (dirSides + dirForward).normalized;

        if (canMove)
        {
            rb.AddForce(move * speed / Time.deltaTime);
        }

        directionPos = transform.position + (storeDir * input.horizontal) + (cam.forward * input.vertical);

        Vector3 dir = directionPos - transform.position;
        dir.y = 0;

        //Finding angle
        float angle = Vector3.Angle(transform.forward, dir);

        float animValue = Mathf.Abs(input.horizontal) + Mathf.Abs(input.vertical);

        animValue = Mathf.Clamp01(animValue);

        anim.SetFloat("Forward", animValue);
        anim.SetBool("LockOn", false);

        if(input.horizontal != 0 || input.vertical != 0)
        {
            if(angle != 0 && canMove)
            {
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            }
        }

    }

    void HandleMovementLockOn()
    {
        Transform camHolder = cam.parent.parent;

        Vector3 camForward = Vector3.Scale(camHolder.forward, new Vector3(1,0,1)).normalized;
        Vector3 camRight = Vector3.Scale(camHolder.right, new Vector3(1, 0, 1)).normalized;

        Vector3 move = input.vertical * camForward + input.horizontal * cam.right;

        Vector3 moveForward = input.vertical * camForward;
        Vector3 moveSideways = input.horizontal * cam.right;

        rb.AddForce((moveForward +moveSideways).normalized * speed / Time.deltaTime);

        ConvertMoveInputAnPassItToAnimator(move);
    }

    void ConvertMoveInputAnPassItToAnimator(Vector3 move)
    {
        Vector3 localMove = transform.InverseTransformDirection(move);
        float turnAmount = localMove.x;
        float forwardAmount = localMove.z;
        
        if(turnAmount != 0)
        {
            turnAmount *= 2;
        }

        anim.SetBool("LockOn", true);
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Sideways", forwardAmount, 0.1f, Time.deltaTime);
    }

    void HandleRotationOnLock()
    {
        Vector3 lookPos = Enemies[curTarget].position;

        Vector3 lookDir = lookPos - transform.position;

        lookDir.y = 0;

        Quaternion rot = Quaternion.LookRotation(lookDir);
        rb.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }*/
}

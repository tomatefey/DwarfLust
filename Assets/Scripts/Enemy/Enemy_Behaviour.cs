using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour {

    [Header ("Component")]
    NavMeshAgent agent;
    Transform target;
    Animator anim;
    Rigidbody rb;

    BoxCollider meleeColl;
    EnemyMeleeCollider meleeCollider;


    [Header("Movement")]
    public float minDist;
    float forwardAmount;

    bool isGrounded;
    public bool isMeleeAttack;

    Vector3 move;
    public float moveSpeedMultiplier;

    float stationaryTurnSpeed;
    float movingTurnSpeed;
    public float turnAmount;

    public float groundCheckDistance;

    [Header("Combat")]
    float combatCounter;
    public float timeCombat;

    public float damage;


    Vector3 groundNormal;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        meleeCollider = GameObject.FindGameObjectWithTag("Enemy_Melee").GetComponent<EnemyMeleeCollider>();
        meleeColl = GameObject.FindGameObjectWithTag("Enemy_Melee").GetComponent<BoxCollider>();
        meleeColl.enabled = false;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {


        if(target != null)
            agent.SetDestination(target.position);

        if(agent.remainingDistance > agent.stoppingDistance)
            Move(agent.desiredVelocity);
        else
        {
            Move(Vector3.zero);
        }
            
        if(agent.remainingDistance < minDist)
        {
            UpdateCombat();
        }

        UpdateAnimator();
	}

    public void Move(Vector3 move)
    {

        
        if(move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        ApplyExtraTurnRotation();
        
        
        UpdateAnimator();
    }
    

    void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }


    public void OnAnimatorMove()
    {
        if(isGrounded && Time.deltaTime > 0)
        {
            Vector3 v;

            if(agent.remainingDistance > agent.stoppingDistance * 2)  v = (anim.deltaPosition * moveSpeedMultiplier * 2) / Time.deltaTime;
            else v = (anim.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            v.y = rb.velocity.y;
            rb.velocity = v;
        }
    }

    public void UpdateCombat()
    {
        

        if(combatCounter > timeCombat)
        {
            isMeleeAttack = true;
            meleeColl.enabled = true;
            meleeCollider.SetDamage(damage);
            combatCounter = 0;
        }
        if(!isMeleeAttack)
        {
            combatCounter += Time.deltaTime;
            meleeColl.enabled = false;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
        
        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
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

    void UpdateAnimator()
    {
        // update the animator parameters
        if(agent.remainingDistance > agent.stoppingDistance * 2) anim.SetFloat("Forward", forwardAmount * 2.0f, 0.2f, Time.deltaTime);
        else anim.SetFloat("Forward", forwardAmount, 0.2f, Time.deltaTime);

        anim.SetBool("OnGround", isGrounded);

        anim.SetBool("isMeleeAttack", isMeleeAttack);

        if (isGrounded && move.magnitude > 0)
        {
            anim.speed = moveSpeedMultiplier;
        }
        else
        {
            anim.speed = 1;
        }
    }
}

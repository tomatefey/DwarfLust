using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour {

    [Header ("Component")]
    NavMeshAgent agent;
    Transform target;
    Animator anim;


    [Header("Movement")]
    public float minDist;
    float forwardAmount;

    bool isGrounded;
    bool isMeleeAttack;

    Vector3 move;
    float animSpeedMultiplier;
    

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if(Vector3.Distance(this.transform.position, target.position) > minDist)
        {
            forwardAmount = 1;
            agent.Resume();
        }
        else
        {
            forwardAmount = 0;
            agent.Stop();
        }
        agent.destination = target.position;

        UpdateAnimator();
	}

    void UpdateAnimator()
    {
        // update the animator parameters
        anim.SetFloat("Forward", forwardAmount, 0.2f, Time.deltaTime);

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

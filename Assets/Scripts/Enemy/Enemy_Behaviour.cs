using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour {

    [Header ("Component")]
    NavMeshAgent agent;
    Transform target;


    [Header("Movement")]
    public float minDist;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if(Vector3.Distance(this.transform.position, target.position) > minDist)
        {
            agent.Resume();
        }
        else
        {
            agent.Stop();
        }
        agent.destination = target.position;
	}
}

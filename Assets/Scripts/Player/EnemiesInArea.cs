using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemiesInArea : MonoBehaviour {

    Transform enemyTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetEnemyGameObject()
    {
        return enemyTarget;
    }
}

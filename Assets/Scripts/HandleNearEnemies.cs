using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNearEnemies : MonoBehaviour {

    PlayerController player;
    GameInputs input;

    List<Transform> targets = new List<Transform>();
    public GameObject[] go;

    [HideInInspector]public Vector3 target;
    public string enemyTag;
    public int enemyIndex = 1;

    GameObject Win_UI;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<GameInputs>();
    }
	
	// Update is called once per frame
	void Update () {
        AddAllEnemies();
        UpdateTarget();
	}

    void UpdateTarget()
    {
        ChangeTarget();

        target = go[enemyIndex].transform.position;

        for (int i = 0; i < go.Length; i++)
        {
            if(i == enemyIndex)
            {
                go[i].GetComponent<Enemy_Health>().isTarget = true;
            }
            else
            {
                go[i].GetComponent<Enemy_Health>().isTarget = false;
            }
        }

        if (targets.Count > 0)
        {
            Vector3 direction = targets[enemyIndex].position - transform.position;
            direction.y = 0;

            float distance = Vector3.Distance(transform.position, targets[enemyIndex].position);


            Vector3 targetPos = direction.normalized * distance / 4;
            targetPos += transform.position;
            target = targetPos;

            if (distance > 20)
            {
                player.lockTarget = false;
            }
        }

        

        
    }

    void ChangeTarget()
    {
        if (input.changeTargetInput)
        {
            enemyIndex++;
        }

        if (enemyIndex > targets.Count - 1)
        {
            enemyIndex = 0;
        }
        else if (enemyIndex < 0)
        {
            enemyIndex = targets.Count - 1;
        }
    }

    void AddAllEnemies()
    {
        targets.Clear();

        go = GameObject.FindGameObjectsWithTag(enemyTag);

        for(int i = 0; i < go.Length; i++)
        {
            GameObject enemy = go[i];
            AddTarget(enemy.transform);
        }
    }

    void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }
}

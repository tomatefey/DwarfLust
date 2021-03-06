﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HordeManager : MonoBehaviour {

    [Header("Objects")]
    public int indexArray;
    public GameObject prefabBerserker;
    int index;
    GameObject[] enemyBerserker;
    HandleNearEnemies nearEnemies;
    
    GameObject spawner_01;

    [Header("Horde")]
    int hordeIndex;
    int spawns;

    [Header("Counter")]
    float timeCounter = 0;
    float maxCounter = 0;

    [Header("UI")]
    Text timeCounter_UI;
    GameObject winUI;

	// Use this for initialization
	void Start () {
        index = 0;
        spawner_01 = GameObject.FindGameObjectWithTag("Spawner");

        timeCounter_UI = GameObject.FindGameObjectWithTag("TimeCounter_UI").GetComponent<Text>();
        winUI = GameObject.FindGameObjectWithTag("Win_UI");

        nearEnemies = GameObject.FindGameObjectWithTag("Player").GetComponent<HandleNearEnemies>();

        enemyBerserker = new GameObject[indexArray];
        for(int i = 0; i < indexArray; i++)
        {
            enemyBerserker[i] = Instantiate(prefabBerserker, this.transform.position, Quaternion.identity) as GameObject;
            enemyBerserker[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(timeCounter > maxCounter)
        {
            hordeIndex++;
            timeCounter = 0;
            maxCounter = 10 * hordeIndex;
            NextHorde();
        }
        else
        {
            timeCounter += Time.deltaTime;
        }

        timeCounter_UI.text = timeCounter.ToString();

        if(hordeIndex > 4 && nearEnemies.targets.Count < 1)
        {
            winUI.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            winUI.SetActive(false);
        }
	}

    void NextHorde()
    {
        for(int i = 0; i < hordeIndex; i++)
        {
            if (hordeIndex < 5)
            {
                enemyBerserker[index].transform.position = spawner_01.transform.position;
                enemyBerserker[index].SetActive(true);

                index++;
                if (index >= enemyBerserker.Length) index = 0;
            }
        }
            
    }
}

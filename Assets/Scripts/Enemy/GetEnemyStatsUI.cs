using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetEnemyStatsUI : MonoBehaviour {
    
    Transform cam;
    PlayerController player;
    Enemy_Health enemy;

    public Transform canvas;
    public float distance = 6;
    Image image;
    public Image image1;
    Material mat;

    float health;

	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); ;
        enemy = GetComponentInParent<Enemy_Health>();
        image = GetComponent<Image>();
        mat = GetComponent<Material>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        health = enemy.GetHealthUI();
        image.fillAmount = health / 100;
        canvas.LookAt(cam);
        if (player.lockTarget)
        {
            if (enemy.isTarget)
            {
                image.enabled = true;
                image1.enabled = true;
            }
            else
            {
                image.enabled = false;
                image1.enabled = false;
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < distance)
            {
                image.enabled = true;
                image1.enabled = true;
            }
            else
            {
                image.enabled = false;
                image1.enabled = false;
            }
        }

        
    }
}

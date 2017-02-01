using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerStatsUI : MonoBehaviour {

    public enum states {HEALTH, STAMINA}
    public states type;

    Player_Stats player;

    Image image;

    float health;
    float stamina;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Stats>();
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (type){
            case states.HEALTH:
                health = player.GetPlayerHealthUI();
                image.fillAmount = health / 100;
                break;

            case states.STAMINA:
                stamina = player.GetPlayerStaminaUI();
                image.fillAmount = stamina / 100;
                break;
        }

        
	}
}

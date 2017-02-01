using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour {

    [Header("Health")]
    public float maxHealth;
    float health;

    [Header("Stamina")]
    public float maxStamina;
    float stamina;
    public float staminaRate;
    float subtractStamina;

    [Header("Invulneravility")]
    public float maxInv = 1.2f;
    float invCounter;



    // Use this for initialization
    void Start () {
        health = maxHealth;
        stamina = maxStamina;
        invCounter = maxInv;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RecieveDmg(10);
        }

        if (stamina >= 100) stamina = 100;
        if (stamina < 0) stamina = 0;
        else stamina += staminaRate* Time.deltaTime;

        isInvulnerable();
    }
    

    public void RecieveDmg(float dmg)
    {
        if (invCounter == maxInv)
        {
            health -= dmg;
            invCounter -= Time.deltaTime;
        }
    }

    void isInvulnerable()
    {
        if (invCounter < maxInv) invCounter -= Time.deltaTime;
        if (invCounter < 0) invCounter = maxInv;
    }

    public void RecieveStamina(float minus)
    {
        stamina -= minus;
    }

    public float GetPlayerHealthUI()
    {
        return health;
    }
    
    public float GetPlayerStaminaUI()
    {
        return stamina;
    }

    public bool HasStamina(float minus)
    {
        float newSta = stamina;
        newSta -= minus;
        if (newSta >= 0) return true;
        else return false;
    }
}

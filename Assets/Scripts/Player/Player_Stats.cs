using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour {

    PlayerController playerCont;
    GameInputs input;

    [Header("Health")]
    public float maxHealth;
    float health;

    Text potionNumberUI;
    public float potionRestoreValue;
    public float potionNumber;

    [Header("Coin")]
    Text coinUI;
    float coins;

    [Header("Stamina")]
    public float maxStamina;
    float stamina;
    public float staminaRate;
    float subtractStamina;


    [Header("Invulneravility")]
    public float maxInv = 1.2f;
    float invCounter;

    GameObject loseUI;

    public Material matColor;
    public Color normalColor;
    public Color damagedColor;


    // Use this for initialization
    void Start () {
        health = maxHealth;
        stamina = maxStamina;
        invCounter = maxInv;

        playerCont = GetComponent<PlayerController>();
        input = GetComponent<GameInputs>();

        potionNumberUI = GameObject.FindGameObjectWithTag("Potion_UI").GetComponent<Text>();
        coinUI = GameObject.FindGameObjectWithTag("Coin_UI").GetComponent<Text>();
        
        loseUI = GameObject.FindGameObjectWithTag("Lose_UI");
        loseUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            loseUI.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (input.potionInput)
        {
            if(potionNumber > 0)
            {
                RestoreHealth(potionRestoreValue);
                potionNumber--;

            }
            
        }
        potionNumberUI.text = potionNumber.ToString();
        coinUI.text = coins.ToString();

        if (stamina >= 100) stamina = 100;
        if (stamina < 0) stamina = 0;
        else stamina += staminaRate* Time.deltaTime;

        isInvulnerable();

        if(matColor.color != normalColor) {
            matColor.color = Color.Lerp(matColor.color, normalColor, 1 * Time.deltaTime);
        } 
    }
    

    public void RecieveDmg(float dmg)
    {
        if (invCounter == maxInv && !playerCont.isDodge && !playerCont.godMode)
        {
            health -= dmg;
            invCounter -= Time.deltaTime;
            matColor.color = damagedColor;
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

    void RestoreHealth(float value)
    {
        for(float i = value; health <= maxHealth && i > 0; i--)
        {
            health ++;
        }
    }

    public void GetMoney(int value)
    {
        coins += value;
    }
}

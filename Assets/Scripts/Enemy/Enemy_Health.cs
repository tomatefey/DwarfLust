using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public float maxHealth;
    public float health;

    Animator anim; 

    [Header("Invulneravility")]
    public float maxInv = 1.2f;
    float invCounter;

    public bool isTarget;

    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(true);
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        isInvulnerable();

        if (health <= 0) Death();
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
    }

    void isInvulnerable()
    {
        if (invCounter < maxInv) invCounter -= Time.deltaTime;
        if (invCounter < 0) invCounter = maxInv;
    }

    public void RecieveDmg(float dmg)
    {
        if (invCounter == maxInv)
        {
            health -= dmg;
            invCounter -= Time.deltaTime;
        }
    }

    public float GetHealthUI()
    {
        return health;
    }

}

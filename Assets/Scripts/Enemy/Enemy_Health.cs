﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public float maxHealth;
    public float health;

    Animator anim; 

    [Header("Invulneravility")]
    public float maxInv = 1.2f;
    float invCounter;

    ParticleController partCont;

    public bool isTarget;

    public GameObject coin;

    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        health = maxHealth;

        partCont = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<ParticleController>();

    }

    // Update is called once per frame
    void Update()
    {
        isInvulnerable();

        if (health <= 0) Death();
    }

    public void Death()
    {
        Instantiate(coin, this.transform.position, Quaternion.identity);
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
            anim.SetTrigger("Hit");
            partCont.SpawnDamagedParticle(this.transform.position);
            invCounter -= Time.deltaTime;
        }
    }

    public float GetHealthUI()
    {
        return health;
    }

}

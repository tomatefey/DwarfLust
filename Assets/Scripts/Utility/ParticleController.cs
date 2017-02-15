using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    public GameObject part_Healing;

    public GameObject part_Damaged;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnHealingParticle(Vector3 pos)
    {
        Instantiate(part_Healing, pos, Quaternion.identity);
    }

    public void SpawnDamagedParticle(Vector3 pos)
    {
        Instantiate(part_Damaged, pos, Quaternion.identity);
    }
}

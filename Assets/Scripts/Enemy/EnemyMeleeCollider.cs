using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCollider : MonoBehaviour {

    float dmg;

    public void SetDamage(float dmg)
    {
        this.dmg = dmg;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!other.GetComponent<PlayerController>().isDodge) other.GetComponent<Player_Stats>().RecieveDmg(this.dmg);
        }
    }
}

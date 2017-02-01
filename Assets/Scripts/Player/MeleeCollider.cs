using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour {

    float dmg;

    public void SetDamage(float dmg)
    {
        this.dmg = dmg;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy_Health>().RecieveDmg(this.dmg);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    float counter;
    public float maxTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if(counter >= maxTime)
        {
            Destroy(this.gameObject);
        }
	}
}

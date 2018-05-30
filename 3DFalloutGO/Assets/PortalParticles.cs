using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParticles : MonoBehaviour {

    public GameObject particles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "THE_character")
        {
            Instantiate(particles, transform);
        }
    }
}

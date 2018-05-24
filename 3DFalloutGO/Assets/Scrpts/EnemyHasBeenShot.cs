using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHasBeenShot : MonoBehaviour {


    public GameObject deathEffect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "my_shot") {
			Destroy (collision.gameObject, 2);
            var particles = Instantiate(deathEffect, transform);
            Destroy(particles.gameObject, 2);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Destroy (gameObject, 2);

        }
	}
}

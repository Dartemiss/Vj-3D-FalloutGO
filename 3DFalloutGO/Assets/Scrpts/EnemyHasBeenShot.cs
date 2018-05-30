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
			Destroy (collision.gameObject, 0.5f);
            var particles = Instantiate(deathEffect, transform);
            Destroy(particles.gameObject, 0.5f);
			//GameObject aux = gameObject.transform.GetChild (0).gameObject;
			GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Destroy (gameObject, 0.5f);

        }
	}
}

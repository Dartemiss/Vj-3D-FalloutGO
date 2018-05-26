using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMelee : MonoBehaviour {

	public Transform mainCharacter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainCharacter.transform.position, transform.position) < 1.2f) {
			Destroy (gameObject);
		}
	}
}

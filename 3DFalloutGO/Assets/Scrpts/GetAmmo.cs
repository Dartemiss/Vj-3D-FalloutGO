using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmo : MonoBehaviour {
	public Transform mainCharacter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, mainCharacter.transform.position) < 1.0f) {
			Destroy (gameObject, 1);
		}
	}
}

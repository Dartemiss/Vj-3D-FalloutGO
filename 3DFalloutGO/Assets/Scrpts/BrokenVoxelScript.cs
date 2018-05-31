using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenVoxelScript : MonoBehaviour {

	public Transform mainCharacter;
	bool lejos = true;
	int numRotura = 0;
	float aux = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lejos && Vector3.Distance (mainCharacter.transform.position, transform.position) < 1.0f) {
			lejos = false;
			gameObject.transform.GetChild(numRotura).gameObject.active= false;
			numRotura = numRotura + 1;
		}
		if (3.0f < Vector3.Distance (mainCharacter.transform.position, transform.position))
			lejos = true;
	
		if (numRotura == 2) {
			mainCharacter.transform.Translate (0, -0.2f, 0);
			aux = aux + 0.1f;
			if (2.5f<aux) {
				SceneManager.LoadScene(2);
			}
		}
	}
}

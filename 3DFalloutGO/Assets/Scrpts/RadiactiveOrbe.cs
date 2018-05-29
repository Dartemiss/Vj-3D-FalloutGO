using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadiactiveOrbe : MonoBehaviour {

	public Transform mainCharacter;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector2 aux1 = new Vector2 (mainCharacter.transform.position.x, mainCharacter.transform.position.z);
		Vector2 aux2 = new Vector2 (transform.position.x, transform.position.z);
		if (Vector2.Distance (aux1, aux2) < 0.5f) {
			SceneManager.LoadScene(3);
		}
	}
}

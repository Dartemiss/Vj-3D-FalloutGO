using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentPalanca : MonoBehaviour {

	public Transform mainCharacter;
	public Transform platform;
	Vector3 newPos;
	bool updown = true;
	bool active = false;
	float aux = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				newPos = hit.transform.position;
				if (Vector3.Distance (mainCharacter.transform.position, newPos) < 2.0f) {
					if (newPos == transform.position) {
						active = true;
					}
				}
			}
		}
		if (active)
			movePlatform ();
	}

	void movePlatform(){
		float altura;
		if (updown)
			altura = 0.1f;
		else
			altura = -0.1f;
		platform.Translate (0, altura, 0);
		transform.Translate (0, altura, 0);
		mainCharacter.Translate (0,altura,0);
		aux = aux + 0.1f;
		if (4.0f < aux) {
			aux = 0.0f;
			updown = !updown;
			active = false;
		}
	}
}

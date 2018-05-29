using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentRadioactiveDesactivador : MonoBehaviour {
	Vector3 newPos;
	public Transform mainCharacter;
	bool active = false;
	public GameObject orbe;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !active) {
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
		if (active) {
			Destroy (orbe);
		}
	}
}

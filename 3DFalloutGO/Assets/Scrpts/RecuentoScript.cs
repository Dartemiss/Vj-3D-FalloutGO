using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuentoScript : MonoBehaviour {
	int howmanyArtifact;
	bool end = false;
	// Use this for initialization
	void Start () {
		howmanyArtifact = 0;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (howmanyArtifact);
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.transform.tag == "artifact") {
					howmanyArtifact++;
				}
			}	
		}

		PlayerPrefs.SetInt ("nuke", howmanyArtifact);

	}
}

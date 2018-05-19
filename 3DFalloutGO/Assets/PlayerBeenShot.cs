using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBeenShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("fgahsuijgha");
		if (collision.gameObject.tag == "shot") {
			Destroy (collision.gameObject);
			SceneManager.LoadScene(1);

		}
	}
}

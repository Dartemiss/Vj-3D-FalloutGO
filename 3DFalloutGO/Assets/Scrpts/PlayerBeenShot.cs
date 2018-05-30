using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBeenShot : MonoBehaviour {

	public int lvl = 0;
	bool GODMODE = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("g")){
			GODMODE = !GODMODE;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "shot") {
			Destroy (collision.gameObject);
			if(!GODMODE)
				SceneManager.LoadScene(lvl);

		}
	}
}

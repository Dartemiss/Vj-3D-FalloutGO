﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	public Transform mainCharacter;
	//public int nextLvl;
    public GameObject winPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainCharacter.position, transform.position) < 1.0f) {
            winPanel.SetActive(true);
			int nuke = PlayerPrefs.GetInt ("nuke");
			if (nuke == 3) {
				SceneManager.LoadScene (8);
			}
			else
				SceneManager.LoadScene (7);
			//SceneManager.LoadScene(nextLvl);
		}
	}
}

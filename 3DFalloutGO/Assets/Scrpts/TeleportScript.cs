﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {
	public Transform mainCharacter;
	public Transform whereToGo;
	float timer = 0.0f;
	float timerMax = 1.5f;
	public float eulerRotation = 0.0f;
    bool soundplayed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainCharacter.transform.position, transform.position) < 1.0f) {
			timer += Time.deltaTime;
            if (!soundplayed)
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                soundplayed = true;
            }
			if (timerMax < timer) {
				mainCharacter.transform.position = new Vector3 (whereToGo.position.x, whereToGo.position.y + 0.51f, whereToGo.position.z);
				mainCharacter.transform.Rotate (0, eulerRotation, 0);
                
			}
		}
	}
		
}

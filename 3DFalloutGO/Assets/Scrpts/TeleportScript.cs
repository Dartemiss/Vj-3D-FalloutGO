using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {
	public Transform mainCharacter;
	public Transform whereToGo;
	float timer = 0.0f;
	float timerMax = 3.0f;
	public float eulerRotation = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainCharacter.transform.position, transform.position) < 1.0f) {
			timer += Time.deltaTime;
			if (timerMax < timer) {
				mainCharacter.transform.position = new Vector3 (whereToGo.position.x, whereToGo.position.y + 0.51f, whereToGo.position.z);
				mainCharacter.transform.Rotate (0, eulerRotation, 0);
			}
		}
	}
		
}

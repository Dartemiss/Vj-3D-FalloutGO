using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentPalanca : MonoBehaviour {

	public Transform mainCharacter;
	public Transform platform;
	public int typeButton = 0;
	Vector3 newPos;
	public bool updown = true;
	bool active = false;
	float aux = 0.0f;
	public float howmany = 4.0f;
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
                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                    }
				}
			}
		}
		if (active && typeButton == 0)
			movePlatformY ();
		else if (active && typeButton == 1)
			movePlatformZ ();
		else if (active && typeButton == 2)
			movePlatformX ();

	}

	void movePlatformY(){
		float altura;
		if (updown)
			altura = 0.1f;
		else
			altura = -0.1f;
		platform.Translate (0, altura, 0);
		if(Mathf.Abs(Vector3.Distance(mainCharacter.position , platform.position))<2.0f){
			transform.Translate (0, altura, 0);
			mainCharacter.Translate (0,altura,0);

		}
		aux = aux + 0.1f;
		if (howmany < aux) {
			aux = 0.0f;
			updown = !updown;
			active = false;
		}
	}

	void movePlatformZ(){
		float dist;
		if (updown)
			dist = 0.1f;
		else
			dist = -0.1f;
		platform.Translate (dist, 0, 0);
		//transform.Translate();
		aux = aux + 0.1f;
		if(howmany< aux){
			aux = 0.0f;
			updown = !updown;
			active = false;
		}
	}

	void movePlatformX(){
		float dist;
		if (updown)
			dist = 0.1f;
		else
			dist = -0.1f;
		platform.Translate (0, 0, dist);
		//transform.Translate();
		aux = aux + 0.1f;
		if(howmany< aux){
			aux = 0.0f;
			platform.Translate (0, 0, -dist);
			updown = !updown;
			active = false;
		}
	}
}

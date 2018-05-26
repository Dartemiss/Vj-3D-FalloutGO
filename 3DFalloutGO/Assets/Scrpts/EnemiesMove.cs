using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMove : MonoBehaviour {

	int numEnemies = 2;
	public Transform mainCharacter;
	Transform enemy;
	Vector3 newPos;
	bool moving = false;
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
				if ((Vector3.Distance (mainCharacter.transform.position, newPos)) < 5.0f && (2.0f <Vector3.Distance (mainCharacter.transform.position, newPos))) {
					moving = true;
				}
			}
		}
		if (moving) {
			moveEnemies ();
		}
		
	}

	void moveEnemies(){
		aux = aux + 0.1f;
		for(int i=0;i <numEnemies;++i){
			enemy = gameObject.transform.GetChild (i);
			enemy.transform.Translate (new Vector3 (0, 0, 0.1f));
		}
		if (4.0f < aux) {
			aux = 0.0f;
			moving = false;
			rotateEnemies ();
		}
	}

	void rotateEnemies(){
		for(int i=0;i <numEnemies;++i){
			enemy = gameObject.transform.GetChild (i);
			enemy.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}
}

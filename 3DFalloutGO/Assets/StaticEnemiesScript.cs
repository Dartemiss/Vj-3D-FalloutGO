using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemiesScript : MonoBehaviour {

	public Transform mainCharacter;
	Transform enemy;
	GameObject floor;
	int numEnemies = 1;
	bool dead = false;
	Vector3[] floorLocations = new Vector3[1];
	// Use this for initialization
	void Start () {
		for (int i = 0; numEnemies > i; ++i) {
			enemy = gameObject.transform.GetChild (i);
			Vector3 auxiliarPos = new Vector3 (enemy.transform.position.x, enemy.transform.position.y + 2.0f, enemy.transform.position.z);
			RaycastHit hit;
			float distance = 100.0f;
			Debug.Log ("hola");
			if (Physics.Raycast (auxiliarPos, Vector3.down, out hit, distance)) {
				floorLocations[i] = hit.transform.position;
				Debug.Log (hit.transform.position);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; numEnemies > i; ++i) {
			enemy = gameObject.transform.GetChild (i);
			checkAndKill ();
			checkClick ();
		}
	}

	void checkAndKill(){
		Vector3 dirFromAtoB = (mainCharacter.transform.position - enemy.position).normalized;
		float dotProd = Vector3.Dot (dirFromAtoB, enemy.forward);
		if (dotProd > 0.9 && !dead) {
			//enemy.Rotate (0, 90, 0);
			if(Vector3.Distance (mainCharacter.transform.position, enemy.position) <= 4.0f){
				mainCharacter.transform.Rotate(-90.0f,0,0);
				dead = true;
			}
		}
	}

	void checkClick(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				Vector3 newPos = hit.transform.position;
				if (Vector3.Distance (mainCharacter.transform.position, newPos) < 5.0f) {
					for(int i = 0;numEnemies >i;++i){
						if (floorLocations [i] == newPos) {
							Debug.Log (floorLocations [i]);
							enemy = gameObject.transform.GetChild (i);
							enemy.transform.Rotate (90.0f, 0, 0);
						}
					}
				}
			}
		}
	}
}

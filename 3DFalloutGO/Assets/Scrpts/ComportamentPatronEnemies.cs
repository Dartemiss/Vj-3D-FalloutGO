using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamentPatronEnemies : MonoBehaviour {
	public Transform platPositions;
	public Transform mainCharacter;
	public GameObject shot;
	float speed = 8.0f;
	bool dead = false;
	Vector3 newPos;
	Vector3 currentPos;
	int whereimgoing = 1;
	bool moving = false;
	public int direction = 2;
	int posneg = 1;
	public int numberPlats;
	public int mylvl;
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
				if ((Vector3.Distance (mainCharacter.transform.position, newPos)) < 5.0f && (2.0f < Vector3.Distance (mainCharacter.transform.position, newPos))) {
					moving = true;
					currentPos = platPositions.transform.GetChild (whereimgoing).position;
					if (1.0f < Mathf.Abs (currentPos.x - transform.position.x)) {
						/*
						if (direction != 0) {
							if (posneg == 1)
								transform.Rotate (0, 90, 0);
							else
								transform.Rotate (0, -90, 0);
						}*/
						transform.LookAt (currentPos);
						direction = 0;	
					} else if (1.0f < Mathf.Abs (currentPos.y - transform.position.y)) {
						transform.LookAt (currentPos);
						direction = 1;
					} else if (1.0f < Mathf.Abs (currentPos.z - transform.position.z)) {
						/*
						if (direction != 2) {
							if (posneg == 1)
								transform.Rotate (0, 90, 0);
							else
								transform.Rotate (0, -90, 0);
						}*/
						transform.LookAt (currentPos);
						direction = 2;
					}
				}
			}
		}

		if (moving)
			moveEnemy ();
		if (Vector3.Distance (transform.position, mainCharacter.position) < 1.0f) {
			SceneManager.LoadScene(mylvl);
		}
	}


	void moveEnemy(){
		if (direction == 0) {
			transform.Translate (0f, 0, 0.1f);
		} else if (direction == 1) {
			transform.Translate (0, 0.1f, 0);
		} else if (direction == 2) {
			transform.Translate (0, 0, 0.1f);
		}

		if (Vector3.Distance (transform.position, currentPos) < 0.65f) {
			moving = false;
			whereimgoing = whereimgoing + posneg;
			if (whereimgoing == -1) {
				posneg = 1;
				transform.Rotate (0, 180, 0);
				whereimgoing = whereimgoing + 2;
			}
			if (whereimgoing == numberPlats) {
				posneg = -1;
				transform.Rotate (0, 180, 0);
				whereimgoing = whereimgoing - 2;
			}

		}
	}

	void checkAndKill(){
		Vector3 dirFromAtoB = (mainCharacter.transform.position - transform.position).normalized;
		float dotProd = Vector3.Dot (dirFromAtoB, gameObject.transform.forward);
		if (dotProd > 0.9 && !dead) {
			//enemy.Rotate (0, 90, 0);
			if(Vector3.Distance (mainCharacter.transform.position, transform.position) <= 4.0f){
				//mainCharacter.transform.Rotate(-90.0f,0,0);
				dead = true;
				GameObject obj = Instantiate (shot, transform.position + Vector3.forward, shot.transform.rotation);
				obj.GetComponent<Rigidbody>().velocity = speed * Vector3.forward;
			}
		}
	}

}

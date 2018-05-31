using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CuchillasVerticales : MonoBehaviour {

	public Transform serraPositions;
	public Transform mainCharacter;
	Vector3 newPos;
	Vector3 currentPos;
	int whereimgoing = 1;
	bool moving = false;
	public int direction = 1;
	int posneg = 1;
	public int numberPlats;
	public int xgir;
	public int ygir;
	public int zgir;
	public bool lado = false;
	bool GODMODE = false;
	public int numLvl;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("g")){
			GODMODE = !GODMODE;
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				newPos = hit.transform.position;
				if ((Vector3.Distance (mainCharacter.transform.position, newPos)) < 5.0f && (2.0f < Vector3.Distance (mainCharacter.transform.position, newPos))) {
					moving = true;
					currentPos = serraPositions.transform.GetChild (whereimgoing).position;
					if (1.0f < Mathf.Abs (currentPos.x - transform.position.x)) {
						if (!lado) {
							if (direction != 0)
								transform.Rotate (0, 0, xgir);
							direction = 0;	
						} else {
							transform.LookAt(currentPos);
							direction = 2;
							transform.Rotate (0, 0, 90);
						}

					} else if (1.0f < Mathf.Abs (currentPos.y - transform.position.y)) {
						if (!lado) {
							if (direction != 1)
								transform.Rotate (0, 0, ygir);
							direction = 1;
						} else {
							transform.LookAt(currentPos);
							direction = 2;
							transform.Rotate (0, 0, 90);
						}
					} else if (1.0f < Mathf.Abs (currentPos.z - transform.position.z)) {
						if (!lado) {
							if (direction != 2)
								transform.Rotate (0, 0, zgir);
							direction = 2;
						} else {
							transform.LookAt(currentPos);
							direction = 2;
							transform.Rotate (0, 0, 90);
						}

					}
				}
			}
		}

		if (moving)
			moveSerra ();

		if (Vector3.Distance (transform.position, mainCharacter.position) < 1.5f) {
			if(!GODMODE)
				SceneManager.LoadScene(numLvl);
		}
	}

	void moveSerra(){
		if (direction == 0) {
			transform.Translate (0, 0.1f, 0);
		} else if (direction == 1) {
			transform.Translate (0, 0.1f, 0);
		} else if (direction == 2) {
			transform.Translate (0, 0, 0.1f);
		}
		if (Vector3.Distance (transform.position, currentPos) < 0.85f) {
			moving = false;
			whereimgoing = whereimgoing + posneg;
			if (whereimgoing == -1) {
				posneg = 1;
				transform.Rotate (0, 0, 180);
				whereimgoing = whereimgoing + 2;
			}
			if (whereimgoing == numberPlats) {
				posneg = -1;
				transform.Rotate (0, 0, 180);
				whereimgoing = whereimgoing - 2;
			}

		}
	}
}

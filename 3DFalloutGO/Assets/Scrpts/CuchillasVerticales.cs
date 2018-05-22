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
	int direction = 1;
	int posneg = 1;
	public int numberPlats;

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
					currentPos = serraPositions.transform.GetChild (whereimgoing).position;
					if (1.0f < Mathf.Abs (currentPos.x - transform.position.x)) {
						if(direction != 0)
							transform.Rotate (0, 0, 270);
						direction = 0;	
					} else if (1.0f < Mathf.Abs (currentPos.y - transform.position.y)) {
						if(direction != 1)
							transform.Rotate (0, 0, 90);
						direction = 1;
					} else if (1.0f < Mathf.Abs (currentPos.z - transform.position.z)) {
						if(direction != 2)
							transform.Rotate (0, 0, 90);
						direction = 2;
					}
				}
			}
		}

		if (moving)
			moveSerra ();

		if (Vector3.Distance (transform.position, mainCharacter.position) < 1.0f) {
			SceneManager.LoadScene(2);
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

		if (Vector3.Distance (transform.position, currentPos) < 0.75f) {
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

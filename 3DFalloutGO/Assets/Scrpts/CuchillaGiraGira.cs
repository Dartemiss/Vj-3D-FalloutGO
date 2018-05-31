using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CuchillaGiraGira : MonoBehaviour {

	public Transform serraPositions;
	public Transform mainCharacter;
	Vector3 newPos;
	Vector3 currentPos;
	int whereimgoing = 1;
	bool moving = false;
	int direction = 0;
	int posneg = 1;
	public int numberPlats;
	bool GODMODE = false;
	public int numLvl;
    public GameObject losePanel;
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
						//if(direction != 0)
							//transform.Rotate (0, 270, 0);
						transform.LookAt(currentPos);
						direction = 0;	
					} else if (1.0f < Mathf.Abs (currentPos.y - transform.position.y)) {
						transform.LookAt(currentPos);
						direction = 1;
					} else if (1.0f < Mathf.Abs (currentPos.z - transform.position.z)) {
						//if(direction != 2)
							//transform.Rotate (0, 90, 0);
						transform.LookAt(currentPos);
						direction = 2;
					}
				}
			}
		}

		if (moving)
			moveSerra ();

		if (Vector3.Distance (transform.position, mainCharacter.position) < 1.0f) {
            if (!GODMODE)
            {
                mainCharacter.GetComponent<GamplayScript>().enabled = false;
                losePanel.SetActive(true);
            }
        }
	}

	void moveSerra(){
		if (direction == 0) {
			transform.Translate (0f, 0, 0.1f);
		} else if (direction == 1) {
			transform.Translate (0, 0.1f, 0);
		} else if (direction == 2) {
			transform.Translate (0, 0, 0.1f);
		}
		if (Vector3.Distance (transform.position, currentPos) < 0.25f) {
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
}

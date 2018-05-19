using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamplayScript : MonoBehaviour {

	public Transform mainCharacter;
	public Transform enemies;
	Transform enemy;
	float speed = 2.0f;
	Vector3 newPos;
	Vector3 actualPos;
	bool moving,jumping,escalar,jumpdown,currently_moving = false;
	float aux = 0.0f;
	int numEnemies = 2;
	bool suelo = true;
	bool updown = true;
	bool jumpFloorToVertical = false;
	bool zz,yy = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !currently_moving) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				//Debug.Log( hit.transform.gameObject.name );
				newPos = hit.transform.position;
				actualPos = mainCharacter.transform.position;
				Debug.Log (hit.transform.gameObject.tag);
				if (Vector3.Distance (mainCharacter.transform.position, newPos) < 5.0f) {
					if (hit.transform.gameObject.tag == "Vertical") {
						if (suelo) {
							if (mainCharacter.transform.position.y < newPos.y) {
								jumping = true;
								mainCharacter.LookAt (new Vector3 (newPos.x, mainCharacter.transform.position.y, newPos.z));
							}
							else {
								jumpFloorToVertical = true;
								zz = true;
								mainCharacter.LookAt (new Vector3 (newPos.x, mainCharacter.transform.position.y, newPos.z));
								mainCharacter.transform.Rotate (0, 180,0f, 0);
							}
							currently_moving = true;
						} else {
							escalar = true;
							currently_moving = true;
							moveEntitiesWall ();
						}
						suelo = false;

					} else if(hit.transform.gameObject.tag == "Floor" || hit.transform.gameObject.tag == "floor_enemy" || hit.transform.gameObject.tag == "broken_floor") {
						if (suelo) {
							moving = true;
							currently_moving = true;
						} else {
							jumpdown = true;
						}
						suelo = true;
						currently_moving = true;
						mainCharacter.LookAt (new Vector3 (newPos.x, mainCharacter.transform.position.y, newPos.z));
					}
				}
			}
		}
		if (moving) {
			moveEntitiesField ();
		} else if (jumping) {
			jumpEntitiesWall ();
		} else if (escalar) {
			moveEntitiesWall ();
		} else if (jumpdown) {
			jumpdownEntitiesWall ();
		} else if (jumpFloorToVertical) {
			floorToVertical ();
		}
	}

	void moveEntitiesField () {
		mainCharacter.transform.Translate(new Vector3 (0,0,0.1f));
		if (Vector3.Distance(new Vector3(mainCharacter.transform.position.x,0.0f,mainCharacter.transform.position.z),new Vector3(newPos.x,0.0f,newPos.z)) < 0.1f) {
			moving = false;
			mainCharacter.transform.Translate(new Vector3 (0,0,0.1f));
			currently_moving = false;
		}
	}

	void jumpEntitiesWall () {
		mainCharacter.transform.Translate(new Vector3 (0,0.1f,0.1f));
		aux = aux + 0.1f;
		if (1.5f < aux && updown) {
			jumping = false;
			aux = 0.0f;
			currently_moving = false;
		}
	}

	void jumpdownEntitiesWall () {
		mainCharacter.transform.Translate(new Vector3 (0,-0.1f,0.1f));
		aux = aux + 0.1f;
		if (1.5f < aux) {
			jumpdown = false;
			aux = 0.0f;
			currently_moving = false;
		}
	}

	void moveEntitiesWall () {
		int direction = goUpDownRightLeft ();
		if(direction == 0)
			mainCharacter.transform.Translate(new Vector3 (0.1f,0,0));
		else if(direction == 1)
			mainCharacter.transform.Translate(new Vector3 (-0.1f,0,0));
		else if(direction == 2)
			mainCharacter.transform.Translate(new Vector3 (0,0.1f,0));
		else if(direction == 3)
			mainCharacter.transform.Translate(new Vector3 (0,-0.1f,0));

		if (direction == 3 || direction == 2) {
			if (Vector3.Distance (new Vector3 (0.0f, mainCharacter.transform.position.y, 0.0f), new Vector3 (0.0f, newPos.y, 0.0f)) < 0.1f) {
				escalar = false;
				currently_moving = false;
			}
		} else {
			if (Vector3.Distance (new Vector3 (mainCharacter.transform.position.x, 0.0f, 0.0f), new Vector3 (newPos.x, 0.0f, 0.0f)) < 0.1f) {
				escalar = false;
				currently_moving = false;
			}
		}
	}

	void floorToVertical(){
		if (zz) {
			mainCharacter.transform.Translate (0, 0, -0.1f);
		} else if (yy) {
			mainCharacter.transform.Translate (0,-0.1f,0);
		}
		if (zz && !yy) {
			if (newPos.z + 1.0f < mainCharacter.transform.position.z) {
				zz = false;
				yy = true;
			}
		} else if (!zz && yy) {
			if (mainCharacter.transform.position.y <= newPos.y) {
				zz = false;
				yy = false;
			}
		} else {
			jumpFloorToVertical = false;
		}
	}


	//Looks in which direction had clicked the player and assign a number for each direction
	// Right = 0, Left = 1, Up = 2, Down = 3
	int goUpDownRightLeft(){
		float dif = Mathf.Abs (newPos.y - actualPos.y);
		if (dif <= 0.2f) {
			if (actualPos.x < newPos.x)
				return 1;
			else
				return 0;
		
		} else{ 
			if (actualPos.y < newPos.y)
				return 2;
			else
				return 3;
	
		}
	}

}

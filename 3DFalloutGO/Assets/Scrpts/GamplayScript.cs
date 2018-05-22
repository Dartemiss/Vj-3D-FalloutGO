using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamplayScript : MonoBehaviour {

	//public Transform mainCharacter;
	public GameObject shot;
	Transform enemy;
	float speed = 8.0f;
	Vector3 newPos;
	Vector3 actualPos;
	bool moving,jumping,escalar,jumpdown,jumpup,currently_moving = false;
	float aux = 0.0f;
	int numEnemies = 2;
	int numAmmo = 1;
	bool suelo = true;
	bool updown = true;
	bool jumpFloorToVertical = false;
	bool zz,yy = false;
	bool up;
	int numBullets = 0;
	public Camera myCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//myCamera.transform.position = transform.position;
		if (Input.GetMouseButtonDown (0) && !currently_moving) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				newPos = hit.transform.position;
				if (0 < numBullets && hit.transform.gameObject.tag == "enemy" ) {
					realShotTime ();
				}
				actualPos = transform.position;
				if (Vector3.Distance (transform.position, newPos) < 5.0f) {
					if (hit.transform.gameObject.tag == "Vertical") {
						if (suelo) {
							if (transform.position.y < newPos.y) {
								jumping = true;
								transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
							}
							else {
								jumpFloorToVertical = true;
								zz = true;
								transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
								transform.Rotate (0, 180,0f, 0);
							}
							currently_moving = true;
						} else {
							escalar = true;
							currently_moving = true;
							moveEntitiesWall ();
						}
						suelo = false;

					} else if(hit.transform.gameObject.tag == "Floor" || hit.transform.gameObject.tag == "floor_enemy" || hit.transform.gameObject.tag == "broken_floor" || hit.transform.gameObject.tag == "floor_ammo" || hit.transform.gameObject.tag == "enemy") {
						if (suelo) {
							moving = true;
							currently_moving = true;
						} else {
							if (transform.position.y < newPos.y) {
								jumpup = true;
								up = true;
							}
							else
								jumpdown = true;
						}
						suelo = true;
						currently_moving = true;
						transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
						if (hit.transform.gameObject.tag == "floor_ammo") {
							numBullets++;
							hit.transform.gameObject.tag = "Floor";
						}
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
		} else if (jumpup) {
			jumpupEntitiesWall ();
		}
		else if (jumpFloorToVertical) {
			floorToVertical ();
		}
	}

	void moveEntitiesField () {
		transform.Translate(new Vector3 (0,0,0.1f));
		if (Vector3.Distance(new Vector3(transform.position.x,0.0f,transform.position.z),new Vector3(newPos.x,0.0f,newPos.z)) < 0.1f) {
			moving = false;
			transform.Translate(new Vector3 (0,0,0.1f));
			currently_moving = false;
		}
	}

	void jumpEntitiesWall () {
		transform.Translate(new Vector3 (0,0.1f,0.1f));
		aux = aux + 0.1f;
		if (1.5f < aux && updown) {
			jumping = false;
			aux = 0.0f;
			currently_moving = false;
		}
	}

	void jumpdownEntitiesWall () {
		transform.Translate(new Vector3 (0,-0.1f,0.1f));
		aux = aux + 0.1f;
		if (1.5f < aux) {
			jumpdown = false;
			aux = 0.0f;
			currently_moving = false;
		}
	}

	void jumpupEntitiesWall (){
		if(up)
			transform.Translate (0,0.1f,0);
		else
			transform.Translate (0,0,0.1f);
		aux = aux + 0.1f;
		if (2.5f < aux && up) {
			aux = 0.0f;
			up = false;
		}
		if (2.5f < aux && !up) {
			jumpup = false;
			aux = 0.0f;
			currently_moving = false;
		}
	}

	void moveEntitiesWall () {
		int direction = goUpDownRightLeft ();
		if(direction == 0)
			transform.Translate(new Vector3 (0.1f,0,0));
		else if(direction == 1)
			transform.Translate(new Vector3 (-0.1f,0,0));
		else if(direction == 2)
			transform.Translate(new Vector3 (0,0.1f,0));
		else if(direction == 3)
			transform.Translate(new Vector3 (0,-0.1f,0));

		if (direction == 3 || direction == 2) {
			if (Vector3.Distance (new Vector3 (0.0f, transform.position.y, 0.0f), new Vector3 (0.0f, newPos.y, 0.0f)) < 0.1f) {
				escalar = false;
				currently_moving = false;
			}
		} else {
			if (Vector3.Distance (new Vector3 (transform.position.x, 0.0f, 0.0f), new Vector3 (newPos.x, 0.0f, 0.0f)) < 0.1f) {
				escalar = false;
				currently_moving = false;
			}
		}
	}

	void floorToVertical(){
		if (zz) {
			transform.Translate (0, 0, -0.1f);
		} else if (yy) {
			transform.Translate (0,-0.1f,0);
		}
		if (zz && !yy) {
			if (newPos.z + 1.0f < transform.position.z) {
				zz = false;
				yy = true;
			}
		} else if (!zz && yy) {
			if (transform.position.y <= newPos.y) {
				zz = false;
				yy = false;
			}
		} else {
			jumpFloorToVertical = false;
			currently_moving = false;
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

	void realShotTime(){
		transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
		if (Mathf.Abs (newPos.y - transform.position.y) < 0.2) {
			//0 forward, 1 back, 2 right 3 left
			Vector3 dir = Vector3.back;
			if (Mathf.Abs (newPos.z - transform.position.z) < 0.5) {
				if (newPos.x < transform.position.x)
					dir = Vector3.left;
				else
					dir = Vector3.right;
			}
			else if (Mathf.Abs (newPos.x - transform.position.x) < 0.5){
					if (newPos.z < transform.position.z)
						dir = Vector3.back;
					else
						dir = Vector3.forward;
			}
			GameObject obj = Instantiate (shot, transform.position + dir , shot.transform.rotation);
			obj.GetComponent<Rigidbody>().velocity = 8.0f * dir;
			--numBullets;

			}
			
	}
}

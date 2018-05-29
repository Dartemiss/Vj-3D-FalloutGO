using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamentPerseguidor : MonoBehaviour {

	public Transform mainCharacter;
	bool active = false;
	bool moving,jumping,escalar,jumpdown,jumpup,currently_moving = false;
	float aux = 0.0f;
	bool suelo = true;
	bool updown = true;
	bool zz,yy = false;
	bool up;
	bool jumpFloorToVertical = false;
	bool first = true;
	public Transform firstDelante;
	public Transform secondDelante;
	Vector3 newPos;
	Vector3 actualPos;
	Vector3 firstPos;
	Vector3 secondPos;

	public int numLvl = 3;
	public Transform tp;

	string currentWhere;
	string firstWhere;
	string secondWhere;
	Vector3 predatorAux;
	// Use this for initialization
	void Start () {
		firstPos = firstDelante.position;
		secondPos = secondDelante.position;
		firstWhere = firstDelante.tag;
		secondWhere = secondDelante.tag;
		//predatorAux = new Vector3 (transform.position.x, 0.51f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (!active)
			checkPJ ();
		else {
			followPJ ();

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
			} else if (jumpFloorToVertical) {
				floorToVertical ();
			}
			if (Vector3.Distance (transform.position, tp.position) < 1.0f) {
				Destroy (gameObject);
			}
		}
		if (Vector3.Distance (transform.position, mainCharacter.position) < 1.0f) {
			SceneManager.LoadScene(numLvl);
		}
	}

	void checkPJ(){
		Vector3 dirFromAtoB = (mainCharacter.transform.position - transform.position).normalized;
		float dotProd = Vector3.Dot (dirFromAtoB, transform.forward);
		float dist = Vector3.Distance (mainCharacter.transform.position, transform.position);
		if ( dotProd>0.9 && 10.0f> dist ) {
			active = true;
		}
	}

	void followPJ(){

		if (Input.GetMouseButtonDown (0) && !currently_moving) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if( Physics.Raycast( ray, out hit, 100 ) )
			{

				if (Vector3.Distance (mainCharacter.transform.position, hit.transform.position) < 5.0f) {
					newPos = firstPos;
					firstPos = secondPos;
					secondPos = hit.transform.position;
					actualPos = transform.position;
					currentWhere = firstWhere;
					firstWhere = secondWhere;
					secondWhere = hit.transform.gameObject.tag;
					if (currentWhere == "Vertical") {
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

					} else if(currentWhere == "Floor" || currentWhere == "floor_enemy" || currentWhere == "broken_floor") {
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
						}
					}
				}
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
			transform.Translate(new Vector3 (-0.1f,0,0));
		else if(direction == 1)
			transform.Translate(new Vector3 (0.1f,0,0));
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

}

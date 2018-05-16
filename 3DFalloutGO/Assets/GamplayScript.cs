﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamplayScript : MonoBehaviour {

	public Transform mainCharacter;
	public Transform enemies;
	Transform enemy;
	float speed = 2.0f;
	Vector3 newPos;
	Vector3 actualPos;
	bool moving,jumping,escalar,jumpdown = false;
	float aux = 0.0f;
	int numEnemies = 2;
	bool suelo = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				//Debug.Log( hit.transform.gameObject.name );
				newPos = hit.transform.position;
				if (Vector3.Distance (mainCharacter.transform.position, newPos) < 5.0f) {
					if (hit.transform.gameObject.tag == "Vertical") {
						if (suelo) {
							jumping = true;
						} else {
							escalar = true;
							actualPos = mainCharacter.transform.position;
							moveEntitiesWall ();
						}
						suelo = false;
						mainCharacter.LookAt (new Vector3 (newPos.x, mainCharacter.transform.position.y, newPos.z));
					} else {
						if (suelo) {
							moving = true;	
						} else {
							jumpdown = true;
						}
						suelo = true;
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
		}
	}

	void moveEntitiesField () {
		mainCharacter.transform.Translate(new Vector3 (0,0,0.1f));
		moveEnemies ();
		if (Vector3.Distance(new Vector3(mainCharacter.transform.position.x,0.0f,mainCharacter.transform.position.z),new Vector3(newPos.x,0.0f,newPos.z)) < 0.1f) {
			moving = false;
			mainCharacter.transform.Translate(new Vector3 (0,0,0.1f));
			rotateEnemies();
		}
	}

	void jumpEntitiesWall () {
		mainCharacter.transform.Translate(new Vector3 (0,0.1f,0.1f));
		moveEnemies ();
		aux = aux + 0.1f;
		if (1.5f < aux) {
			jumping = false;
			aux = 0.0f;
			//mainCharacter.transform.Translate(new Vector3 (0,0,0.1f));
			rotateEnemies();
		}
	}

	void jumpdownEntitiesWall () {
		mainCharacter.transform.Translate(new Vector3 (0,-0.1f,0.1f));
		moveEnemies ();
		aux = aux + 0.1f;
		if (1.5f < aux) {
			jumpdown = false;
			aux = 0.0f;
			rotateEnemies();
		}
	}

	void moveEntitiesWall () {
		int direction = goUpDownRightLeft ();
		//if(direction == 0)
			mainCharacter.transform.Translate(new Vector3 (0.1f,0,0));
		//else if(direction == 1)
			mainCharacter.transform.Translate(new Vector3 (-0.1f,0,0));
		if(direction == 2)
			mainCharacter.transform.Translate(new Vector3 (0,0.1f,0));
		else if(direction == 3)
			mainCharacter.transform.Translate(new Vector3 (0,-0.1f,0));
		moveEnemies ();
		if (Vector3.Distance(new Vector3(0.0f,mainCharacter.transform.position.y,0.0f),new Vector3(0.0f,newPos.y,0.0f)) < 0.1f) {
			escalar = false;
			//mainCharacter.transform.Translate(new Vector3 (0,0.1f,0));
			rotateEnemies();
		}
	}

	void moveEnemies(){
		for(int i=0;i <numEnemies;++i){
			enemy = enemies.gameObject.transform.GetChild (i);
			enemy.transform.Translate (new Vector3 (0, 0, 0.1f));
		}
	}

	void rotateEnemies(){
		for(int i=0;i <numEnemies;++i){
			enemy = enemies.gameObject.transform.GetChild (i);
			enemy.transform.Rotate (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}

	//Looks in which direction had clicked the player and assign a number for each direction
	// Right = 0, Left = 1, Up = 2, Down = 3
	int goUpDownRightLeft(){
		//if (newPos.y - actualPos.y <= 0.2f) {
		//	if (actualPos.x < newPos.x)
		//		return 0;
		//	else
		//		return 1;
		
		//} else{ 
			if (actualPos.y < newPos.y)
				return 2;
			else
				return 3;
	
		//}
	}

}

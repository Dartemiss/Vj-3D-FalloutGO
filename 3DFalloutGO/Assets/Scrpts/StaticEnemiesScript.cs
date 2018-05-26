using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemiesScript : MonoBehaviour {

	public Transform mainCharacter;
	Transform enemy;
	GameObject floor;
	public GameObject shot;
	public int numEnemies = 4;
	bool dead = false;
	public float speed;
	Vector3[] floorLocations = new Vector3[20];
	// Use this for initialization
	void Start () {
		for (int i = 0; numEnemies > i; ++i) {
			enemy = gameObject.transform.GetChild (i);
			Vector3 auxiliarPos = new Vector3 (enemy.transform.position.x, enemy.transform.position.y + 2.0f, enemy.transform.position.z);
			RaycastHit hit;
			float distance = 100.0f;
			if (Physics.Raycast (auxiliarPos, Vector3.down, out hit, distance)) {
				floorLocations[i] = hit.transform.position;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		numEnemies = gameObject.transform.childCount - 1;
		for (int i = 0; numEnemies > i; ++i) {
			enemy = gameObject.transform.GetChild (i);
			checkAndKill ();
			//checkClick ();
		}
	}

	void checkAndKill(){
		Vector3 dirFromAtoB = (mainCharacter.transform.position - enemy.position).normalized;
		float dotProd = Vector3.Dot (dirFromAtoB, enemy.forward);

		if (dotProd > 0.9 && !dead) {
			//enemy.Rotate (0, 90, 0);
			if(Vector3.Distance (mainCharacter.transform.position, enemy.position) <= 4.1f){
				dead = true;
				mainCharacter.gameObject.GetComponent<GamplayScript >().enabled = false;
				if (enemy.transform.rotation.eulerAngles.y == 270) {
					GameObject obj = Instantiate (shot, enemy.transform.position + Vector3.left, shot.transform.rotation);
					obj.GetComponent<Rigidbody> ().velocity = speed * Vector3.left;
				} else if(enemy.transform.rotation.eulerAngles.y == 0) {
					GameObject obj = Instantiate (shot, enemy.transform.position + Vector3.forward, shot.transform.rotation);
					obj.GetComponent<Rigidbody> ().velocity = speed * Vector3.forward;
				} else if(enemy.transform.rotation.eulerAngles.y == 90) {
					GameObject obj = Instantiate (shot, enemy.transform.position + Vector3.right, shot.transform.rotation);
					obj.GetComponent<Rigidbody> ().velocity = speed * Vector3.right;
				} else if(enemy.transform.rotation.eulerAngles.y == 180) {
					GameObject obj = Instantiate (shot, enemy.transform.position + Vector3.back, shot.transform.rotation);
					obj.GetComponent<Rigidbody> ().velocity = speed * Vector3.back;
				}

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
						if (Vector3.Distance ( floorLocations [i], newPos) < 1.0f) {
							enemy = gameObject.transform.GetChild (i);
							Destroy (enemy.gameObject);
							Vector3[] aux = new Vector3[20];
							int offset = 0;
							for(int j = 0;numEnemies >j;++j){
								if (i == j)
									offset = 1;
								aux [j] = floorLocations [j + offset];
											
							}
							--numEnemies;
							floorLocations = aux;	
						}
					  
					}
				}
			}
		}
	}
}

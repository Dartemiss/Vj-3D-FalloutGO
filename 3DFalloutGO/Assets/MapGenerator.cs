using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject recta;
	public Transform ini;
	Vector3 actualpos;
	// Use this for initialization
	void Start () {
		Vector3 iniPos = ini.position;
		for (int i = 1; i < 5; i++) {
			Vector3 posini = ini.position;
			actualpos = new Vector3 (posini.x, posini.y, posini.z - 4.0f * i);
			GameObject obj = Instantiate (recta,actualpos,ini.rotation);
		}
		actualpos = new Vector3 (actualpos.x, actualpos.y + 2.0f, actualpos.z - 2.0f);
		GameObject obj2 = Instantiate(recta,actualpos,ini.rotation);
		obj2.transform.Rotate(90.0f,0.0f,0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

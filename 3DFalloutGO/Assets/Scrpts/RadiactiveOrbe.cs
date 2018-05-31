using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadiactiveOrbe : MonoBehaviour {

	public Transform mainCharacter;
	bool GODMODE = false;
    public GameObject losePanel;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("g")){
			GODMODE = !GODMODE;
		}
		Vector2 aux1 = new Vector2 (mainCharacter.transform.position.x, mainCharacter.transform.position.z);
		Vector2 aux2 = new Vector2 (transform.position.x, transform.position.z);
		if (Vector2.Distance (aux1, aux2) < 0.5f) {
            if (!GODMODE)
            {
                mainCharacter.GetComponent<GamplayScript>().enabled = false;
                losePanel.SetActive(true);
            }
        }
	}
}

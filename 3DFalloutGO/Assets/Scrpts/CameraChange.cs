using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

    //La idea seria desactivar la camera que estigués activa i activar la nova que vulguem, 
    //pero encara he de trobar com saber quina és la que estava activa. De moment desactivo totes excepte al que volem
    public GameObject camActivate;
    public GameObject camDeactivate1;
    public GameObject camDeactivate2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        //if (collision.gameObject.tag == "characterTag"){
        camActivate.SetActive(true);
        camDeactivate1.SetActive(false);
        camDeactivate2.SetActive(false);
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public Vector3 tp = new Vector3(4.45f,28.1f,8.0f);
	Vector3 offsetCam = new Vector3 (-1.0f,12.0f,-18.0f);

	Animator m_Animator;
	public Text ammoText;
	public GameObject acquireBobblehead;
    public GameObject acquireArtifact;
	public Text bobbleRecountText;
    public Text artifRecountText;
    int bobbleRecount = 0;
    int artifRecount = 0;
	bool availableAnimation = true;
	public GameObject imageBobbNoAcq;
	public GameObject imageBobbYesAcq;
    public GameObject imageBobbNoAcq2;
    public GameObject imageBobbYesAcq2;
    public GameObject imageBobbNoAcq3;
    public GameObject imageBobbYesAcq3;
    public GameObject imageArtifNoAcq;
    public GameObject imageArtifYesAcq;
    Transform vaultBoy;
	bool first = true;

	bool GODMODE = false;

	// Use this for initialization
	void Start () {
		m_Animator = GetComponentInChildren<Animator>();
		updateUIBullets();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("t")){
			transform.position = new Vector3 (tp.x, tp.y + 0.51f, tp.z);
		}
		if(Input.GetKeyDown("g")){
			GODMODE = !GODMODE;
			if (GODMODE)
            {
                numBullets = 50;
                updateUIBullets();
            }
				
			else{
                numBullets = 0;
                updateUIBullets();
            }
				
		}
		if (Input.GetMouseButtonDown (0) && !currently_moving) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				availableAnimation = true;
				bool isshoting = false;
				newPos = hit.transform.position;
				if (0 < numBullets && hit.transform.gameObject.tag == "enemy" && 5.0f < Vector3.Distance (transform.position, newPos)) {
					if(Mathf.Abs(newPos.z - transform.position.z)< 1.0f || Mathf.Abs(newPos.x - transform.position.x)< 1.0f )
						realShotTime ();
					isshoting = true;
				}

				actualPos = transform.position;
				if (!isshoting) {
					if (Vector3.Distance (transform.position, newPos) < 5.0f) {
						if (hit.transform.gameObject.tag == "Vertical") {
							if (suelo) {
								if (transform.position.y < newPos.y) {
									jumping = true;
									transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
									m_Animator.Play ("ClimbFirst");
								} else {
									jumpFloorToVertical = true;
									zz = true;
									transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
									transform.Rotate (0, 180, 0f, 0);
								}
								currently_moving = true;
							} else {
								escalar = true;
								currently_moving = true;
								//moveEntitiesWall ();
							}
							suelo = false;

						} else if (hit.transform.gameObject.tag == "Floor" || hit.transform.gameObject.tag == "floor_enemy" || hit.transform.gameObject.tag == "broken_floor" || hit.transform.gameObject.tag == "floor_ammo" || hit.transform.gameObject.tag == "enemy") {
							if (suelo) {
								moving = true;
								currently_moving = true;
							} else {

								if (transform.position.y < newPos.y) {
									jumpup = true;
									m_Animator.Play ("ClimbUpFinal");
									up = true;
								} else
									jumpdown = true;
								if (!first) {
									vaultBoy = gameObject.transform.GetChild (0);
									vaultBoy.position = new Vector3 (vaultBoy.position.x, vaultBoy.position.y + 7.0f/5.0f, vaultBoy.position.z);
									first = true;
								}

								if (transform.position.y < newPos.y)
								{
									jumpup = true;
									m_Animator.Play("ClimbUpFinal");
									up = true;
								}
								else
								{
									jumpdown = true;
									m_Animator.Play("ClimbDownFloor");
								}

							}
							suelo = true;
							currently_moving = true;
							transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
							if (hit.transform.gameObject.tag == "floor_ammo") {
								numBullets++;
                                StartCoroutine(ammoSound());
                                hit.transform.gameObject.tag = "Floor";
								updateUIBullets ();
							}
						}
					}
				}
				if (hit.transform.tag == "collectible")
				{
					acquireBobblehead.SetActive(true);
					AudioSource audioBobble = hit.transform.gameObject.GetComponent<AudioSource>();
					audioBobble.Play();
					StartCoroutine(bobbleDisappear(hit));
					++bobbleRecount;

					if (bobbleRecount == 1)
					{
                        bobbleRecountText.text = bobbleRecount.ToString() + " Bobbleheads";
                        imageBobbNoAcq.SetActive(false);
						imageBobbYesAcq.SetActive(true);
					}
                    if (bobbleRecount == 2)
                    {
                        bobbleRecountText.text = bobbleRecount.ToString() + " Bobbleheads";
                        imageBobbNoAcq2.SetActive(false);
                        imageBobbYesAcq2.SetActive(true);
                    }
                    if (bobbleRecount == 3)
                    {
                        bobbleRecountText.text = bobbleRecount.ToString() + " Bobbleheads";
                        imageBobbNoAcq3.SetActive(false);
                        imageBobbYesAcq3.SetActive(true);
                    }
                }
                else if (hit.transform.tag == "artifact")
                {
                    acquireArtifact.SetActive(true);
                    AudioSource audioArtif = hit.transform.gameObject.GetComponent<AudioSource>();
                    audioArtif.Play();
                    StartCoroutine(artifDisappear(hit));
                    ++artifRecount;
                    if (artifRecount == 1)
                    {
                        artifRecountText.text = artifRecount.ToString() + "/1 Artifact pieces";
                        imageArtifNoAcq.SetActive(false);
                        imageArtifYesAcq.SetActive(true);
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
		} else if (jumpFloorToVertical) {
			floorToVertical ();
			if (availableAnimation)
			{
				m_Animator.Play("Slide");
				availableAnimation = false;
			}
		}
	}

	void moveEntitiesField () {
		m_Animator.Play("Walk");
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
			if (first) {
				vaultBoy = gameObject.transform.GetChild (0);
				vaultBoy.position = new Vector3 (vaultBoy.position.x, vaultBoy.position.y - 7.0f/5.0f, vaultBoy.position.z);
				first = false;
			}
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
		//Debug.Log (direction);
		if (direction == 0)
		{
			transform.Translate(new Vector3(-0.1f, 0, 0));
			if (availableAnimation)
			{
				m_Animator.Play("ClimbLeft");
				availableAnimation = false;
			}
		}
		else if (direction == 1)
		{
			transform.Translate(new Vector3(0.1f, 0, 0));
			if (availableAnimation)
			{
				m_Animator.Play("ClimbRight");
				availableAnimation = false;
			}
		}
		else if (direction == 2)
		{
			transform.Translate(new Vector3(0, 0.1f, 0));
			if (availableAnimation)
			{
				m_Animator.Play("ClimbMiddle");
				availableAnimation = false;
			}
		}
		else if (direction == 3)
		{
			transform.Translate(new Vector3(0, -0.1f, 0));
			if (availableAnimation)
			{
				m_Animator.Play("ClimbDown");
				availableAnimation = false;
			}
		}
		if (direction == 3 || direction == 2) {
			if (Vector3.Distance (new Vector3 (0.0f, transform.position.y, 0.0f), new Vector3 (0.0f, newPos.y, 0.0f)) < 0.1f) {
				escalar = false;
				currently_moving = false;
			}
		} else {
			if (Vector3.Distance (new Vector3 (transform.position.x, 0.0f, 0.0f), new Vector3 (newPos.x, 0.0f, 0.0f)) < 0.1f || Vector3.Distance (new Vector3 (0.0f, 0.0f, transform.position.z), new Vector3 (0.0f, 0.0f, newPos.z)) < 0.1f) {
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
		aux = aux + 0.1f;
		if (zz && !yy) {
			if (2.5f< aux) {
				zz = false;
				yy = true;
				aux = 0.0f;
			}
		} else if (!zz && yy) {
			if (2.5f < aux) {
				zz = false;
				yy = false;
				aux = 0.0f;
			}
		} else {
			jumpFloorToVertical = false;
			currently_moving = false;
			if (first) {
				vaultBoy = gameObject.transform.GetChild (0);
				vaultBoy.position = new Vector3 (vaultBoy.position.x, vaultBoy.position.y - 7.0f/5.0f, vaultBoy.position.z);
				first = false;
			}
		}
	}


	//Looks in which direction had clicked the player and assign a number for each direction
	// Right = 0, Left = 1, Up = 2, Down = 3
	int goUpDownRightLeft(){
		float dif = Mathf.Abs (newPos.y - actualPos.y);
		if (dif <= 0.2f) {
			if (transform.rotation.eulerAngles.y < 5.2f && -5.2 < transform.rotation.eulerAngles.y) {
				if (actualPos.x < newPos.x)
					return 1;
				else
					return 0;
			} else if ((transform.rotation.eulerAngles.y < 185.2f && 175.8f < transform.rotation.eulerAngles.y)) {
				if (actualPos.x < newPos.x)
					return 0;
				else
					return 1;
			} else if ((transform.rotation.eulerAngles.y < 95.2f && 85.8f < transform.rotation.eulerAngles.y)) {
				if (actualPos.z < newPos.z)
					return 0;
				else
					return 1;
			} else {
				if (actualPos.z < newPos.z)
					return 1;
				else
					return 0;
			}

		} else{ 
			if (actualPos.y < newPos.y)
				return 2;
			else
				return 3;

		}
	}

	public IEnumerator shootingDelay(Vector3 dir)
	{
		yield return new WaitForSeconds(0.8f);

        var audioS = GetComponents<AudioSource>();
		audioS[0].Play();
		Vector3 alturaGun = new Vector3 (0, 1.5f, 0);
		GameObject obj = Instantiate(shot, transform.position + dir + alturaGun, shot.transform.rotation);
		obj.GetComponent<Rigidbody>().velocity = 8.0f * dir;

	}

	public IEnumerator bobbleDisappear(RaycastHit hit)
	{
		yield return new WaitForSeconds(1.0f);
		Destroy(hit.transform.gameObject);
	}

    public IEnumerator artifDisappear(RaycastHit hit)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(hit.transform.gameObject);
    }

    public IEnumerator ammoSound()
    {
        yield return new WaitForSeconds(0.3f);
        var audioS = GetComponents<AudioSource>();
        audioS[1].Play();
    }

    void realShotTime(){
		transform.LookAt (new Vector3 (newPos.x, transform.position.y, newPos.z));
		if (Mathf.Abs (newPos.y - transform.position.y) < 1.0f) {
			//0 forward, 1 back, 2 right 3 left
			Vector3 dir = Vector3.back;
			if (Mathf.Abs (newPos.z - transform.position.z) < 1.0) {
				if (newPos.x < transform.position.x)
					dir = Vector3.left;
				else
					dir = Vector3.right;
			}
			else if (Mathf.Abs (newPos.x - transform.position.x) < 1.0f){
				if (newPos.z < transform.position.z)
					dir = Vector3.back;
				else
					dir = Vector3.forward;
			}
			m_Animator.Play("Shoot");
			StartCoroutine(shootingDelay(dir));
			--numBullets;
			updateUIBullets();
		}

	}

	void updateUIBullets()
	{
		ammoText.text = "Ammo: " + numBullets.ToString();
	}
}

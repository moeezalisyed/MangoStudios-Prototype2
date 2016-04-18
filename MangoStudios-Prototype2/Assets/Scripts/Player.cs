using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class Player : MonoBehaviour {

  // for object info
	private GameManager owner;
	private playerModel	model; //the model associated with this script
	private int	playerType = 0;	//	type of the player
	private int	health = 10;	//health of the player
	private float speed = 0f; //speed movement for this character
	public int direction = 0; // direction of the object
	//private Material mat;	// material (for texture)
	private bool firstRun = true; //	to check whether it is a current player or a shadow character
	//public BoxCollider2D playerbody;		//the boxcollider associated with this script
  // for shadow
	private int	shadowitr = 0; //the index in the shadow lists given above
	private List<Vector3> shadowMovements = new List<Vector3>();	//Positions at every update step during firstrun
	private List<bool>  shadowFiring = new List<bool>();	//Stores true/false to keep track of whether a bullet was fired
	private List<bool> shadowAbility = new List<bool>();	//Stores true/false to keep track of whether a SPAB was used
	private List<int>	shadowDirections	= new List<int>();   //Keeps track of where the player was pointing
  // for cooldown
	public float abilTime = 0.0f;
	public float clock= 0f;		//tracking for cooldown
	public float cdbufF= 0f;		//time until firing cooldown is over
	public float cdF= 0f;		//cooldown length for firing
	public	float cdbufA= 0f;		//time until ability cooldown is over
	public float cdA= 0f;	//cooldown length for ability
	public bool isdead = false;
	public bool usingAbility = false;



	// Use this for initialization
	public void init (int type, GameManager manager) {
		//initilise the health to 10
		this.health = 10;
		this.owner = manager;
		this.playerType = type;

	if (type == 0){
      //triangle
			this.cdF = 0.5f;
			this.cdA = 1.5f;


    } else if (type == 1){
      //circle
			this.cdF = 0f;
			this.cdA = 1.5f;

    } else if (type == 2){
      //square
			this.cdF = 0.8f;
			this.cdA = 1.5f;

    }


		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<playerModel> ();
		model.init (this.playerType, this);
		this.tag = "Player";




	}

	public void die(){
		firstRun = false;
		this.isdead = true;
		this.owner.THEBOSS.bossHealth = 100;
		foreach (Player x in owner.shadowPlayers) {
			x.shadowitr = 0;
		}
	}

	public bool isDead(){
		return this.isdead;

	}

	public void damage(int dam){
		health-= dam;
		if (health == 0) {
			this.die ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "ForceField") {
			this.abilTime = 3.0f;
		}



		if (this.playerType == 0) {
			//triangle


			if (other.name == "BossBullet") {
				this.damage (1);
			} else if (other.name == "BossBeam") {
				this.damage (2);
			
			} else if (other.name == "BossBlade") {
				this.damage (5);

			} 

		
		
		} else if (this.playerType == 1) {
			//cirlce
			if (other.name == "BossBullet") {
				this.damage(1);
			} else if (other.name == "BossBeam") {
				this.damage (2);

			} else if (other.name == "BossBlade") {
				this.damage (5);

			} 
		
		} else if (this.playerType == 2) {
			//square
			if (other.name == "BossBullet") {
				if (!usingAbility) {
					this.damage (1);
				}
			} else if (other.name == "BossBeam") {
				if (!usingAbility) {
					this.damage (2);
				}
			} else if (other.name == "BossBlade") {
				if (!usingAbility) {
					this.damage (5);
				}
			} 
		
		}
	}


//	public void cooldown(){}

	public void shoot(){
		if (this.clock - this.cdbufF > this.cdF) {
			this.addBullet (1,0,direction*90);
			this.cdbufF = this.clock;
		}
	}

	public void useAbility(){
		StartCoroutine (usingability ());
	}

	IEnumerator usingability(){
		this.usingAbility = true;
		if (this.playerType == 0) {
			this.cdF = 0.1f;
		}
		yield return new WaitForSeconds (3);
		this.usingAbility = false;
		if (this.playerType == 0) {
			this.cdF = 0.5f;
		}
	}


	private void addBullet(float x, float y, int angle)
	{
		GameObject bulletObject = new GameObject();
		Bullet bullet = bulletObject.AddComponent<Bullet>();
		bullet.transform.position = new Vector3(x, y, 0);
		bullet.init(this);
	}


	void OnGUI(){
		GUI.color = Color.yellow;
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		GUI.skin.box.fontSize = 25;
		string s = "";
		for (int i = 0; i < (this.cdF -this.clock+this.cdbufF) *10 ; i++) {
			s += "I";
		}
		GUI.Box(new Rect (10, 500, 200, 100), s);
		GUI.color = Color.white;
		GUI.skin.box.fontSize = 12;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	}

	public int getType(){
		return this.playerType;
	}

	// Update is called once per frame
	void Update () {
		if (this.abilTime > 0) {
			this.abilTime -= Time.deltaTime;
		}
		this.clock += Time.deltaTime;
		if (this.firstRun) {
			shadowMovements.Add(this.transform.position);

			// Get new positions with ifdirectionkeypressed!
			if (Input.GetKey (KeyCode.RightArrow) ) {
				if (this.playerType != 0) {
					this.direction = 3;
					this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
					this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
				} else {
					if (!this.usingAbility) {
						this.direction = 3;
						this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
						this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
					}
				}
			} 
			if (Input.GetKey (KeyCode.UpArrow) ) {
				
				if (this.playerType != 0) {
					this.direction = 0;
					this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
					this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
				} else {
					if (!this.usingAbility) {
						this.direction = 0;
						this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
						this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
					}
				}
				//above this
			}
			if (Input.GetKey (KeyCode.LeftArrow) ){
				

				//below
				if (this.playerType != 0) {
					this.direction = 1;
					this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
					this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
				} else {
					if (!this.usingAbility) {
						this.direction = 1;
						this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
						this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
					}
				}

				//above
			}
			if (Input.GetKey (KeyCode.DownArrow) ) {
				

				//below
				if (this.playerType != 0) {
					this.direction = 2;
					this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
					this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
				} else {
					if (!this.usingAbility) {
						this.direction = 2;
						this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
						this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
					}
				}

				//above
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				shadowAbility.Add(true);
				this.useAbility ();
			} else {
				shadowAbility.Add (false);
			}


			if (Input.GetKeyDown (KeyCode.Space)) {
				shadowFiring.Add(true);
				this.shoot ();
			} else {
				shadowFiring.Add (false);
			}
				
		} else {
			if (shadowitr >= shadowMovements.Count) {
			
			} 
				else {
				this.model.mat.color = Color.gray;
				//this.mat.shader = Shader.Find("Transparent/Diffuse");
				if (shadowFiring [shadowitr] == true) {
					this.shoot ();
				}
				if (shadowAbility [shadowitr] == true) {
					this.useAbility ();
				}
				this.transform.position = shadowMovements [shadowitr];
				shadowitr++;
			}
		}
	}
}

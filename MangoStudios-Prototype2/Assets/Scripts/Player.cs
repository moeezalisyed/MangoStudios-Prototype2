using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class Player : MonoBehaviour {

  // for object info
	private GameManager owner;
	private playerModel	model; //the model associated with this script
	private int	playerType;	//	type of the player
	private int	health;	//health of the player
	private float speed; //speed movement for this character
	public int direction = 0; // direction of the object
	private Material mat;	// material (for texture)
	private bool firstRun = true; //	to check whether it is a current player or a shadow character
	private PolygonCollider2D polyCollider = null;		//the boxcollider associated with this script
  // for shadow
	private int	shadowitr = 0; //the index in the shadow lists given above
	private List<Vector3> shadowMovements = new List<Vector3>();	//Positions at every update step during firstrun
	private List<bool>  shadowFiring = new List<bool>();	//Stores true/false to keep track of whether a bullet was fired
	private List<bool> shadowAbility = new List<bool>();	//Stores true/false to keep track of whether a SPAB was used
	private List<int>	shadowDirections	= new List<int>();   //Keeps track of where the player was pointing
  // for cooldown

	public float clock;		//tracking for cooldown
	public float cdbufF;		//time until firing cooldown is over
	public float cdF;		//cooldown length for firing
	public	float cdbufA;		//time until ability cooldown is over
	public float cdA;	//cooldown length for ability
	public bool isdead = false;



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


		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);
		name = "char"+playerType;
		this.mat = GetComponent<Renderer> ().material;
		this.mat.shader = Shader.Find ("Sprites/Default");
		//Rename the textures to define them as char1 char2, char0 etc
		this.mat.mainTexture = Resources.Load<Texture2D> ("Textures/char"+playerType);
		this.mat.color = new Color (1, 1, 1, 1);



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
				this.damage(1);
			} else if (other.name == "BossBeam") {
				this.damage (2);

			} else if (other.name == "BossBlade") {
				this.damage (5);

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

	private void addBullet(float x, float y, int angle)
	{
		GameObject bulletObject = new GameObject();
		Bullet bullet = bulletObject.AddComponent<Bullet>();
		bullet.transform.position = new Vector3(x, y, 0);
		bullet.init(this);
	}

	public int getType(){
		return this.playerType;
	}

	// Update is called once per frame
	void Update () {
		this.clock += Time.deltaTime;
		if (this.firstRun) {
			shadowMovements.Add(this.transform.position);

			// Get new positions with ifdirectionkeypressed!
			if (Input.GetKey (KeyCode.RightArrow) ) {
				this.direction = 3;
				this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
				this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
			} 
			if (Input.GetKey (KeyCode.UpArrow) ) {
				this.direction = 0;
				this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
				this.transform.Translate (Vector3.up * 4 * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.LeftArrow) ){
				this.direction = 1;
				this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
				this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.DownArrow) ) {
				this.direction = 2;
				this.transform.eulerAngles = new Vector3 (0, 0, this.direction * 90);
				this.transform.Translate (Vector3.up * 4 * Time.deltaTime);
			}






			if (Input.GetKeyDown (KeyCode.Space)) {
				shadowFiring.Add(true);
				this.shoot ();
			} else {
				shadowFiring.Add (false);
			}
				transform.position = new Vector3 (transform.position.x + speed, transform.position.y + speed);
		} else {
			if (shadowitr >= shadowMovements.Count) {
			} else {
				this.mat.color = Color.gray;
				//this.mat.shader = Shader.Find("Transparent/Diffuse");
				if (shadowFiring [shadowitr] == true) {
					this.shoot ();
				}
				this.transform.position = shadowMovements [shadowitr];
				shadowitr++;
			}
		}
	}
}

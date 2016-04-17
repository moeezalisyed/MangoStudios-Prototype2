using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

  // for object info
	private GameManager owner;
	private PlayerModel	model; //the model associated with this script
	private int	playerType;	//	type of the player
	private int	health;	//health of the player
	private float speed; //speed movement for this character
	private Material mat;	// material (for texture)
	private boolean firstRun = true; //	to check whether it is a current player or a shadow character
	private PolygonCollider2D polyCollider = null;		//the boxcollider associated with this script
  // for shadow
	private int	shadowitr = 0; //the index in the shadow lists given above
	private List<Vector3> shadowPos = new List<Vector3>();	//Positions at every update step during firstrun
	private List<bool>  shadowFiring = new List<bool>();	//Stores true/false to keep track of whether a bullet was fired
	private List<bool> shadowAbility = new List<bool>();	//Stores true/false to keep track of whether a SPAB was used
	private List<int>	shadowDirections	= new List<int>();   //Keeps track of where the player was pointing
  // for cooldown
	private float clock;		//tracking for cooldown
	private float cdbufF;		//time until firing cooldown is over
	private float cdF;		//cooldown length for firing
	private	float cdbufA;		//time until ability cooldown is over
	private float cdA;	//cooldown length for ability



	// Use this for initialization
	public void init (int type, GameManager manager) {
		//initilise the health to 10
		this.health = 10;
		this.owner = manager;
		this.playerType = type;

		/*if (type == 0){
      //triangle

    } else if (type == 1){
      //circle

    } else if (type == 2){
      //square

    }*/


	}

	public void die(){
		firstRun = false;
		this.owner.m.THEBOSS.bossHealth = 100;
		foreach (Player x in owner.m.shadowPlayers) {
			x.model.shadowitr = 0;
		}
	}
	public void damage(){
		health--;
		if (health == 0) {
			this.destroy ();
		}
	}

	public void onTriggerEnter(){}
	public void cooldown(){}

	public void shoot(){
		if (this.clock - this.cdbufF > this.cdF) {
			this.addBullet (1,0,owner.direction*90);
			this.cdbufF = this.clock;
		}
	}

	private void addBullet(float x, float y, int angle)
	{
		GameObject bulletObject = new GameObject();
		Bullet bullet = bulletObject.AddComponent<Bullet>();
		bullet.transform.position = new Vector3(x, y, 0);
		bullet.init(this, x, y, angle);
	}

	// Update is called once per frame
	void Update () {
		this.clock += Time.deltaTime;
		if (this.firstRun) {
			shadowMovements.Add (this.transform.position);
			if (Input.GetKeyDown (KeyCode.Space)) {
				shadowFiring.Add (true);
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

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private GameManager owner;
	private int	health;	//health of the player
	private int	playerType;	//	type of the player 
	private boolean firstRun = true; //	to check whether it is a current player or a shadow character
	private PlayerModel	model; //the model associated with this script
	private float speed; //speed movement for this character 	
	private List<Vector3> shadowPos = new List<Vector3>();	//Stores the positions at every update step when this player was a current player 
	private List<bool>  shadowFiring = new List<bool>();	//Stores true/false to keep track of whether a bullet was fired
	private List<bool> shadowSPAB = new List<bool>();	//Stores true/false to keep track of whether a SPAB was used
	private List<int>	shadowDirections	= new List<int>();//Keeps track of where the player was pointing 
	private float clock;		//tracking for cooldown
	private float cdbufF;		//tracking for cooldown
	private float cdF;		//cooldown for firing
	private	float cdbufA;		//tracking for cooldown
	private float cdA;	//cooldown for abilities
	private int	shadowitr = 0; //the index in the shadow lists given above
	private PolygonCollider2D polyCollider = null;		//the boxcollider associated with this script




	// Use this for initialization
	public void init (int type, GameManager manager) {
		//initilise the health to 10
		this.health = 10;
		this.owner = manager;
		this.playerType = type;

		// We will also intiliase the model here
		//******Fill out the player model stuff here******

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

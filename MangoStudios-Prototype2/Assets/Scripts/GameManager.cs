using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private List<Player> playersInitialised = new List<Player>(); // list of all players
		private int nextPlayer = 0;
		public List<Player> shadowPlayers = new List<Player>(); // list of all shadowplayers
		public Player currentplayer; // the current player
		public Boss THEBOSS; // the current boss
		public Boolean gameover = false; // if the game is over and the player loses
		public Boolean gamewon = false; // if the game is over and the player wins

  
    // Use this for initialization
    void Start()
	{
		// initialise the current player
		GameObject playerObject = new GameObject();
		currentplayer = playerObject.AddComponent<Player> ();
		currentplayer.init (1, this);

		this.playersInitialised.Add (currentplayer);
		nextPlayer = nextplayer ();
		// we pass it an integer to initialise the boss type
		GameObject bossObject = new GameObject();
		THEBOSS = bossObject.AddComponent<Boss> ();
		THEBOSS.init (this);

	}

    // Update is called once per frame
    void Update() {
		if (this.currentplayer.isDead ()) {
			this.iteratePlayer ();
		}

    }



	public void iteratePlayer(){

		// move on to the next player
		shadowPlayers.Add (currentplayer);
		currentplayer = createNextPlayer(nextPlayer);
		//this.playersInitialised.Add (currentplayer);
		nextPlayer = createNextPlayer ();
		THEBOSS.giveFullHealth ();


	}

	public int nextplayer(){
		// will return a random int between 0 and included. 
		return 1;
	}

	public Player createNextPlayer(int nextptype){
		//Create the Next Player using random order generation;
		// Will fill this function out as we go along.
		GameObject plObejct = new GameObject();
		Player x = plObejct.AddComponent<Player> ();
		x.init (nextptype, this);
		return x;
	}

	// this would be to set up the bars to the right
	void OnGUI(){
	// Again, can fill this out as we go along


	}







}

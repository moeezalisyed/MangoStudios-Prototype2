using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class playerModel : MonoBehaviour
{
	private int playerType;
	public Player owner;
	public Material mat;

	public void init(int playerType, Player powner) {
		this.playerType = playerType;
		owner = powner;
	
		transform.parent = owner.transform;
		transform.localPosition = new Vector3 (0, 0, 0);
		name = "PlayerModel";
		mat = GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");
		mat.mainTexture = Resources.Load<Texture2D> ("Textures/char" + this.playerType);
		mat.color = new Color (1, 1, 1, 1);
	
	}

	void OnBecameInvisible() {
		print ("went off screen");
	}

//	void OnTriggerEnter2D(Collider2D other){
//		//print ("col");
//		if (other.name == "Boss") {
//			this.damage ();
//		}
//		if (other.name == "BossBullet") {
//			this.damage ();
//		}
//		if (other.name == "BossBeam") {
//			this.damage ();
//		}
//	}

	void damageTexture(){}

}

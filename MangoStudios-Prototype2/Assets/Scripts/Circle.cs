using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Circle : Player
{
	GameManager owner;
	PlayerModel model;

	private void init(GameManager owner) {
		this.owner = owner;
		//to be balanced
		this.cdF = 0qf;
		this.cdA = 1.5f;

		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);
		name = "Circle";
		this.mat = GetComponent<Renderer> ().material;
		this.mat.shader = Shader.Find ("Sprites/Default");
		this.mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
		this.mat.color = new Color (1, 1, 1, 1);
	}

	void Update(){
		this.clock += Time.deltaTime;
		/*if (this.firstRun) {
				transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
		}*/
	}

	public void useAbility(){}


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


}

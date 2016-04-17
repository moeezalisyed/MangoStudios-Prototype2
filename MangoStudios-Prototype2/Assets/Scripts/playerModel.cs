using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class playerModel : MonoBehaviour
{
	private int playerType;

	public void init(int playerType) {
		this.playerType = playerType;
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

	void OnGUI(){
		GUI.color = Color.yellow;
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		GUI.skin.box.fontSize = 25;
		string s = "";
		for (int i = 0; i < (cd-clock+cdbuf) *10 ; i++) {
			s += "I";
		}
		GUI.Box(new Rect (10, 500, 200, 100), s);
		GUI.color = Color.white;
		GUI.skin.box.fontSize = 12;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	}





}

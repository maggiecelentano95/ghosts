using UnityEngine;
using System.Collections;

public class BluePortal : MonoBehaviour {

	/*
	 * The  portal makes the characters gravity positive
	 * when the character touches the portal's trigger.
	 * It was necessary to create two separate Portals,
	 * one for negative gravity, and one for positive 
	 * gravity. This way, we could separate the two portals
	 * and avoid annoying side effects.
	 * THIS PORTAL IS FOR BLUE PLAYER ONLY
	 * IT WILL NOT EFFECT THE RED PLAYER
	*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "BluePlayer") {
			PlayerController2.instance.FlipGravity (false);
		}
	}
}

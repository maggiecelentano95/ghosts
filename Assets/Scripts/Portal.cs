using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	/*
	 * The  portal makes the characters gravity positive
	 * when the character touches the portal's trigger.
	 * It was necessary to create two separate Portals,
	 * one for negative gravity, and one for positive 
	 * gravity. This way, we could separate the two portals
	 * and avoid annoying side effects.
	*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			PlayerController.instance.FlipGravity (false);
		} else if (other.tag == "BluePlayer") {
			PlayerController2.instance.FlipGravity (false);
		}
	}
}

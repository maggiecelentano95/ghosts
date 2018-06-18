using UnityEngine;
using System.Collections;

public class RedPortal : MonoBehaviour {

	/*
	 * The  portal makes the characters gravity positive
	 * when the character touches the portal's trigger.
	 * It was necessary to create two separate Portals,
	 * one for negative gravity, and one for positive 
	 * gravity. This way, we could separate the two portals
	 * and avoid annoying side effects.
	 * THIS PORTAL IS FOR RED PLAYER ONLY.
	 * It will not effect the Blue player
	*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			PlayerController.instance.FlipGravity (false);
		}
	}
}

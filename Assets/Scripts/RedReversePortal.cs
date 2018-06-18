using UnityEngine;
using System.Collections;

public class RedReversePortal : MonoBehaviour {

	/*
	 * The portal makes the characters gravity negative
	 * when the character touches the portal's collider.
	 * It was necessary to create two separate Portals,
	 * one for negative gravity, and one for positive 
	 * gravity. This way, we could separate the two portals
	 * and avoid annoying side effects.
	 * THIS PORTAL IS FOR RED PLAYER ONLY
	 * IT WILL NOT EFFECT THE BLUE PLAYER
	*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			PlayerController.instance.FlipGravity (true);
		}
	}
}

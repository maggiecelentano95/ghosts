using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {
	/*This script is added to a GameObject that is attached to the 
	 * top of the players' head. It allows the two characters
	 * to ride on top of each other. Without this script,
	 * if one player jumps on top of the other player and the bottom player 
	 * moves, the player on top would fall off. 
	*/



	/*
	void OnTriggerStay2D(Collider2D other){
		PlayerController topPlayer = other.GetComponent<PlayerController>();
		topPlayer.transform.parent = this.transform.parent;
		topPlayer.ChangeMass();

	void OnTriggerExit2D(Collider2D other){
		PlayerController topPlayer = other.GetComponent<PlayerController>();
		topPlayer.transform.parent = null;
		topPlayer.ChangeMassBack();
	}*/


	/*When a player jumps on top of the other, their mass goes to 0
		and they become a child of the character they are on top of.
		The parent-child relationship prevents the top player from falling
		off the bottom player if/when the bottom player moves. The mass of 0
			lets the bottom player jump normally, without being bogged down by 
			the weight of the top player.*/
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "BluePlayer") {
			PlayerController2.instance.transform.parent = PlayerController.instance.transform;
			PlayerController2.instance.ChangeMass ();
		}
		else if (other.tag == "RedPlayer") {
			PlayerController.instance.transform.parent = PlayerController2.instance.transform;
			PlayerController.instance.ChangeMass ();
		}
	}

		/*When the player detaches from the top of the other player,
		 * it goes back to having no parent, and its mass
		 * is returned to normal*/
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "BluePlayer") {
			PlayerController2.instance.ChangeMassBack ();
			PlayerController2.instance.transform.parent = null;
		}
		else if (other.tag == "RedPlayer") {
			PlayerController.instance.ChangeMassBack ();
			PlayerController.instance.transform.parent = null;
		}
	}
}

using UnityEngine;
using System.Collections;

public class BlueBox : MonoBehaviour {
	/*This script is designed to only let the Blue player pass
	 * through the blue portal. The red player's gravity will be
	 * set to 0 to prevent the red player from being able to pass through.
	 * Then, when the red player walks off the blue portal,
	 * its gravity will be set back to normal.
	 * 	 * Note: The RedBox and BlueBox scripts do not actually
	 * change the gravity of the respective players; the
	 * portal scripts do that. These box scripts simply exist to
	 * prevent the red player from falling through the
	 * isTrigger game object of the blue portal,
	 * and to prevent the blue player from falling through the
	 * isTrigger game object of the red portal*/

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			PlayerController.instance.NoGravity ();
		} 
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			PlayerController.instance.ResetGravity ();
		}
	}
}

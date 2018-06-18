using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {

	/*Script for the fruit objects (which act like coins)
	 * If a player touches one, the UI text fruit counter
	 * increases, and the fruit gets destroyed.*/

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.EndsWith ("Player")) {
			GameManager.instance.AddFruit ();
			Destroy (this.gameObject);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public AudioSource pickUpKeyAudio;		//For audio clip when reach door with key

	/*
	 * When either player collides with key, GameManager.instance.hasLevelKey = true;
	*/
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer" || other.tag == "BluePlayer") {
			Debug.Log ("Got key!");
			pickUpKeyAudio.Play ();
			GameManager.instance.PickUpKey ();
			Destroy (this);
			Destroy (this.transform.GetComponentInParent<SpriteRenderer>());
		}
	}
}

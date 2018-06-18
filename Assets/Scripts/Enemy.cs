using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	/*
	 * Note that the "Bad" part of the enemy is always the parent
	 */
	private float speed = 2.5f;
	private Vector3 startingPosition; //The enemy's initial position on the platform
	private Vector3 endingPosition;//How far to the right the emeny should go
	private bool goBack = false; //Control if enemy is moving left or right

	void Awake(){
		startingPosition = this.transform.position;
		//The enemy should only move 5 units to the left before moving back
		endingPosition = startingPosition + new Vector3 (5.0f, 0.0f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		//The parent object (top enemy) controls the movement
		//The child object will follow suit
		//If enemy has no yet rached the ending position, move right
		//else, start moving back left.
		if (this.tag.StartsWith ("Bad")) {
			if (goBack) {
				transform.position += Vector3.left * speed * Time.deltaTime;
				if (transform.position.x < startingPosition.x) {
					goBack = false;
				}
			} else {
				transform.position += Vector3.right * speed * Time.deltaTime;
				if (transform.position.x > endingPosition.x) {
					goBack = true;
				}
			}
		}
	}
	//For audio clip upon player death
	public AudioClip deathmelody;

	//If either player touches the bad part of the enemy (the blue part,)
	//they lose a life. If they touch the good part of the enemy,
	//(the white part), the enemy is destroyed
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			if (this.tag.StartsWith("Bad")) {
				PlayerController.instance.Die ();


			} else if (this.tag.StartsWith("Good")) {
				DestroyEnemy ();
			}
		}else if (other.tag == "BluePlayer") {
			if (this.tag.StartsWith("Bad")) {
				PlayerController2.instance.Die ();


			} else if (this.tag.StartsWith("Good")) {
				DestroyEnemy ();
			}
		}
	}

	/*Destroy the game object, and spawn a fruit in the enemy's place*/
	public void DestroyEnemy(){
		Vector3 spawnFruitAt = transform.parent.transform.position;
		Instantiate (Resources.Load ("fruit"),
			spawnFruitAt,
			Quaternion.Euler (0, 0, 0));
		Destroy (transform.parent.gameObject);
	}
}

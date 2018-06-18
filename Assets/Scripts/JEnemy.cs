using UnityEngine;
using System.Collections;

public class JEnemy : MonoBehaviour {
	/*
	 * Note that the "Bad" part of the enemy is always the parent
	 */
	private float speed = 3.5f;
	private Vector3 startingPosition; // The enemy's initial position on the platform
	private Vector3 endingPosition; //How high we want the enemy to go
	private bool goBack = false; //Control if enemy is moving up or down

	void Awake(){
		startingPosition = this.transform.position;
		//Have the ending position be 1 units above starting position
		if (transform.tag == "JBadBottom") {
			endingPosition = startingPosition + new Vector3 (0.0f, -1.0f, 0.0f);
		} else {
			endingPosition = startingPosition + new Vector3 (0.0f, 1.0f, 0.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		//The parent object (the enemy on top) takes control of the movement
		//The child object (the bottom enemy) follows suit.
		//The jumping enemies, however, are a bit more complicated becuse the child should
		//does not move in unison with the parent, but rather in opposite unison.
		//Ie. when parent moves up, child should move down
		//Note that the child's movement has to be multipled by 2...
		//..this is because the parent's movement affects the child's, and so
		//we must apply double the transformation on the child in order 
		//to counteract this.
		if (this.tag ==  "JBadTop") {
			if (goBack) {
				transform.GetChild (0).position += Vector3.up * 2 * speed * Time.deltaTime;
				transform.position += Vector3.down * speed * Time.deltaTime;
				if (transform.position.y < startingPosition.y) {
					goBack = false;
				}
			} else {
				transform.GetChild (0).position += Vector3.down * 2 * speed * Time.deltaTime;
				transform.position += Vector3.up * speed * Time.deltaTime;
				if (transform.position.y > endingPosition.y) {
					goBack = true;
				}
			}
		} else if (this.tag == "JBadBottom") {
			{
				if (goBack) {
					transform.GetChild(0).position += Vector3.down * 2 * speed * Time.deltaTime;
					transform.position += Vector3.up * speed * Time.deltaTime;
					if (transform.position.y > startingPosition.y) {
						goBack = false;
					}
				} else {
					transform.GetChild(0).position += Vector3.up * 2 * speed * Time.deltaTime;
					transform.position += Vector3.down * speed * Time.deltaTime;
					if (transform.position.y < endingPosition.y) {
						goBack = true;
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "RedPlayer") {
			if (this.tag.StartsWith("JBad")) {
				PlayerController.instance.Die ();
			} else if (this.tag.StartsWith("JGood")) {
				DestroyEnemy ();
			}
		}else if (other.tag == "BluePlayer") {
			if (this.tag.StartsWith("JBad")) {
				PlayerController2.instance.Die ();
			} else if (this.tag.StartsWith("JGood")) {
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

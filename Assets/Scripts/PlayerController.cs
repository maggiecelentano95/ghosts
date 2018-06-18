using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	private Scene scene;
	private Rigidbody2D rigidBody; //character's have rigidbody component
	private Vector3 startingPosition; 
	private float speed = 4.5f; //How fast character can move
	private float jumpForceUp = 7.0f; //How much force to apply when character jumps
	public AudioSource deathAudio;		//For audio clip upon player death
	private bool flipped = false; //gravity starts off normal, so character is not flipped

	void Awake(){
		instance = this;
		rigidBody = GetComponent<Rigidbody2D> ();
		startingPosition = this.transform.position;
		deathAudio = GetComponent<AudioSource>();
	}

	// Use this for initialization
	public void StartGame () {
		this.transform.position = startingPosition; 
	}
	/*Method used for user input to control the characters
	 * Note that the jumpForce's direction is changed,
	 * depending on the character's gravityScale
	*/
	void FixedUpdate ()
	{	
		if (GameManager.instance.currentGameState == GameState.inGame) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				GetComponent<SpriteRenderer> ().flipX = true;
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				GetComponent<SpriteRenderer> ().flipX = false;
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) && IsGrounded ()) {
				ChangeMassBack (); //Make sure mass is 1 before jump
				if (rigidBody.gravityScale < 0) {
					rigidBody.AddForce (Vector2.down * jumpForceUp, ForceMode2D.Impulse);
				} else {
					rigidBody.AddForce (Vector2.up * jumpForceUp, ForceMode2D.Impulse); 
				}
			}
		}
	}

	//Character's have two ground layers: the ground, and the other character
	//This means the character can jump if they are on the ground
	//or on the other character.
	public LayerMask groundlayer;

	bool IsGrounded(){
		if (rigidBody.gravityScale > 0) {
			if (Physics2D.Raycast (this.transform.position, Vector2.down, 0.6f, groundlayer.value)) {
				return true;
			}
		} else {
			if (Physics2D.Raycast (this.transform.position, Vector2.up, 0.6f, groundlayer.value)) {
				return true;
			}
		}

		return false;
	}
		
	/*
	 * A fraction of a second must be waited until gravity can be reversed.
	 * If gravity is reversed too soon, many side-effects occur,
	 * such as the character falling up before they are on the other side
	 * of the ground
	*/

	IEnumerator WaitToFlip(bool flip){
		yield return new WaitForSeconds (0.3f);
		flipped = flip;
		BoxCollider2D collider = rigidBody.gameObject.transform.GetChild (0).GetComponent<BoxCollider2D> ();
		//changes made when gravity is negative
		if (flip) {
			rigidBody.gravityScale = -1.0f;
			collider.offset = new Vector2 (collider.offset.x, -.4f);
			GetComponent<SpriteRenderer> ().flipY = true;
		} else { //changes made when gravity is positive
			rigidBody.gravityScale = 1.0f;
			collider.offset = new Vector2 (collider.offset.x, .4f);
			GetComponent<SpriteRenderer> ().flipY = false;
		}
	}


	//f is true when gravity will be negative
	public void FlipGravity(bool flip){
		StartCoroutine(WaitToFlip (flip));
	}

	public void Kill() {
		//GameManager.instance.GameOver ();
	}

	public void Die(){
		Debug.Log("Dead!");

		// Switch the following two lines if you do/don't want the sound to play
		// if the last life is being lost.
		StartCoroutine (LostLife ());
		GameManager.instance.ReduceLives ();
	}

	//Blink player three times to signal a lost life
	IEnumerator LostLife(){
		//Prevents audio from repeating after losing last life
		if (!(GameManager.instance.currentGameState == GameState.gameOver))
			deathAudio.Play ();
		GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.2f);
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.2f);
		GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.2f);
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.2f);
		GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.2f);
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void AddFruit(){
		Debug.Log ("got fruit!");
	}

	//Allow good jumping when characters are on top of each other
	public void ChangeMass(){
		rigidBody.mass = 0;
	}

	//When player steps off of the other player, make their
	//mass go back to normal
	public void ChangeMassBack(){
		rigidBody.mass = 1;
	}

	//If player stands on a portal that doesn't belong to them, make the
	//gravity for them 0 so they do not fall through the portal
	public void NoGravity(){
		rigidBody.gravityScale = 0.0f;
		rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 0.0f);
	}

	//After the player moves away from the portal that does not belong
	//to them, return their gravity back to what it was
	//before they stepped on the portal
	public void ResetGravity(){
		if (flipped) {
			rigidBody.gravityScale = -1.0f;
		} else {
			rigidBody.gravityScale = 1.0f;
		}
	}
}

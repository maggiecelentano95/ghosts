using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	bool hasKey;
	public AudioSource lockedAudio;		//For audio clip when reach door without key
	public AudioSource openAudio;		//For audio clip when reach door with key

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "RedPlayer" || other.tag == "BluePlayer") {
			Debug.Log ("door collider");
			hasKey = GameManager.instance.HasKey ();
			if (hasKey) { //Door only "opens" when they have the key
				Debug.Log ("Open!");
				StartCoroutine (ToNextLevel ());
			} else {
				Debug.Log ("Locked!");
				lockedAudio.Play ();
			}
		}
			
	}
	/* Plays open-door audio and loads next level via GameManager*/
	IEnumerator ToNextLevel (){
		openAudio.Play ();
		yield return new WaitForSeconds (2f);
		GameManager.instance.CompletedLevel ();
	}
}

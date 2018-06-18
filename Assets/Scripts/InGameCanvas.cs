using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameCanvas : MonoBehaviour {
		public Text lives; //show the lives count
		public Text fruit; //show the fruit count
		public Image key; //show the key icon if they have the key

	    //Continuously update the UI so the correct numbers are shown
		void Update(){
			lives.text = "Lives: " + GameManager.instance.getLives ();
			fruit.text = "Fruit: " + GameManager.instance.getFruit ();

		//If a player got the key, show key sprite in bottom right corner
		if (GameManager.instance.hasLevelKey)
			key.enabled = true;
		else
			key.enabled = false;
		}
	}

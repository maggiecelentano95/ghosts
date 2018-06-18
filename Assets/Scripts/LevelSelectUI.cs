using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour {

	public Text level1; 
	public Text level2;
	public Text level3;
	public Text level4;
	public Text level5;

	//Update the UI so the correct numbers are shown
	void Start(){
		level1.text += " " + PlayerPrefs.GetInt("Level1");
		level2.text += " " + PlayerPrefs.GetInt("Level2");
		level3.text += " " + PlayerPrefs.GetInt("Level3");
		level4.text += " " + PlayerPrefs.GetInt("Level4");
		level5.text += " " + PlayerPrefs.GetInt("Level5");
	}
}

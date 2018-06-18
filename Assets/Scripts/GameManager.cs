using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState{
	menu,
	inGame,
	gameOver,
	levelCompleted,
	pause
}

public class GameManager : MonoBehaviour {

	//Using singleton patter
	public static GameManager instance;

	//All the canvases used in the game
	public Canvas menuCanvas;
	public Canvas inGameCanvas;
	public Canvas gameOverCanvas;
	public Canvas levelCompletedCanvas;
	public Canvas pauseCanvas;

	//Game starts in the menu state
	public GameState currentGameState = GameState.menu;

	private int lives = 3; //Player starts with 3 lives
	private int fruit = 0; //Player starts with 0 fruit
	public bool hasLevelKey = false; //Player starts off not having key
	public Text fruitText; //used to display fruit count at end of level

	void Start(){
		//Upon script start if in "Menu" Scene state set to menu...
		if (SceneManager.GetActiveScene ().name == "Menu")
			currentGameState = GameState.menu;
		//otherwise state is set to inGame
		else
			currentGameState = GameState.inGame;
	}
	
	void Awake(){
		instance = this;
		ResetLives ();
		ResetFruit ();
	}

	// Called to start the game
	public void StartGame () {
		ResetLives ();
		ResetFruit ();
		SceneManager.LoadScene ("Level1");
		//PlayerController.instance.StartGame ();
	}

	//Reload the scene we are in. 
	//Fruit and lives must be reset
	public void RestartLevel(){
		ResetLives ();
		ResetFruit ();
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.name);
			
	}

	//Go back to the menu screen
	public void RestartGame(){
		Scene scene = SceneManager.GetActiveScene ();
		if (scene.name != "Menu") {
			SceneManager.LoadScene ("Menu");
		}
		//SetGameState (GameState.menu);
		ResetLives ();
		ResetFruit ();
	}

	//The following LoadLevel methods
	//were created for the buttons on
	//the Level Select screen
	//of the game.
	public void LevelSelectScreen(){
		SceneManager.LoadScene ("LevelSelect");
	}

	public void LoadLevel1(){
		SceneManager.LoadScene ("Level1");
	}

	public void LoadLevel2(){
		SceneManager.LoadScene ("Level2");
	}

	public void LoadLevel3(){
		SceneManager.LoadScene ("Level3");
	}

	public void LoadLevel4(){
		SceneManager.LoadScene ("Level4");
	}

	public void LoadLevel5(){
		SceneManager.LoadScene ("Level5");
	}

	public void Update(){
	}

	//Called when player dies
	public void GameOver(){
		Debug.Log ("Game State = GameOver");
		SetGameState (GameState.gameOver);
		//SceneManager.LoadScene ("Level1");
	}

	//called when player decides to go back to the menu
	public void BackToMenu(){
		SceneManager.LoadScene ("Menu");
		//SetGameState(GameState.menu);
	}

	//Called when player pauses
	public void Pause(){
		SetGameState (GameState.pause);
	}

	public void UnPause(){
		SetGameState (GameState.inGame);
	}

	//Called when player completes level 
	//See if the high score should be updated for that level
	//Also set the game state to CompletedLevel so that
	//the CompletedLevel canvas gets shown
	public void CompletedLevel(){
		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene.name == "Level1") {
			if (PlayerPrefs.GetInt ("Level1") < fruit) {
				PlayerPrefs.SetInt ("Level1", fruit);
			}
		}
		if (currentScene.name == "Level2") {
			if (PlayerPrefs.GetInt ("Level2") < fruit) {
				PlayerPrefs.SetInt ("Level2", fruit);
			}
		}
		if (currentScene.name == "Level3") {
			if (PlayerPrefs.GetInt ("Level3") < fruit) {
				PlayerPrefs.SetInt ("Level3", fruit);
			}
		}
		if (currentScene.name == "Level4") {
			if (PlayerPrefs.GetInt ("Level4") < fruit) {
				PlayerPrefs.SetInt ("Level4", fruit);
			}
		}
		if (currentScene.name == "Level5") {
			if (PlayerPrefs.GetInt ("Level4") < fruit) {
				PlayerPrefs.SetInt ("Level4", fruit);
			}
		}

		fruitText.text = "F r u i t  G a t h e r e d: " + fruit;
		SetGameState (GameState.levelCompleted);
	}

	//Called when user hits "Continue Journey" button
	//on the LevelCompleted canvas
	//The first 3 levels are the main game, and so the 
	//winning screen is shown when level 3 is completed
	//Level 4 is accessd via the Level Select screen
	public void NextLevel(){
		hasLevelKey = false;
		ResetLives ();
		ResetFruit ();
		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene.name == "Level1") {
			SceneManager.LoadScene ("Level2");
		} else if (currentScene.name == "Level2") {
			SceneManager.LoadScene ("Level3");
		} else if (currentScene.name == "Level3") {
			SceneManager.LoadScene ("WinningScreen");
		} else if (currentScene.name == "Level4") {
			SceneManager.LoadScene ("Level5");
		}

		SetGameState (GameState.inGame);
	}	

	void SetGameState(GameState newGameState){
		if (newGameState == GameState.menu) {
			//setup Unity scene for menu state
			menuCanvas.enabled = true;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			levelCompletedCanvas.enabled = false;
			//pauseCanvas.enabled = false;
		} 
		else if (newGameState == GameState.inGame) {
			//setup Unity scene for inGame state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
			levelCompletedCanvas.enabled = false;
			//pauseCanvas.enabled = false;
		} 
		else if (newGameState == GameState.gameOver) {
			//setup Unity scene for gameOver state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			levelCompletedCanvas.enabled = false;
			//pauseCanvas.enabled = false;
		}
		else if (newGameState == GameState.levelCompleted) {
			//setup Unity scene for gameOver state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			levelCompletedCanvas.enabled = true;
			//pauseCanvas.enabled = false;
		}/*
		else if (newGameState == GameState.pause) {
			//setup Unity scene for gameOver state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			levelCompletedCanvas.enabled = false;
			pauseCanvas.enabled = true;
		}*/

		currentGameState = newGameState; 
	}

	//When fruit is collected, add fruit count by 1
	public void AddFruit(){
		fruit++;
	}
		
	//Reset lives to default (3)
	public void ResetLives(){
		lives = 3;
	}

	//Reset fruit to default count (0)
	public void ResetFruit(){
		fruit = 0;
	}

	//When player hits enemy, subtract life count by 1
	// If the count is 0, it is game over
	public void ReduceLives(){
		lives--;
		if (lives == 0) {
			GameOver ();
		}
	}

	//Return how many lives the players have
	public int getLives(){
		return lives;
	}

	//Return how many fruits the players have
	public int getFruit(){
		return fruit;
	}

	//If players has touched the key, then
	//hasLevelKey is set to true
	public void PickUpKey(){
		hasLevelKey = true;
	}

	//returns the boolean of whether or not playes have the key
	public bool HasKey(){
		return hasLevelKey;
	}
}


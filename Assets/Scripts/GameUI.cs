using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour 
{
	public GameManager manager;

	public Text scoreText;
	public Text livesText;

	public GameObject gameOverScreen;
	public Text gameOverScoreText;
	

	public GameObject winScreen;
	public Text winScoreText;

	void Update ()
	{
		if(!manager.gameOver && !manager.wonGame){					
			scoreText.text = "<b>SCORE</b>  " + manager.score;		
			livesText.text = "<b>LIVES</b>: " + manager.lives;		
		}else{														
			scoreText.text = "";
			livesText.text = "";
		}
	}

	public void SetGameOver ()
	{
		gameOverScreen.SetActive(true);
		gameOverScoreText.text = "<b>YOU ACHIEVED A SCORE OF</b>\n" + manager.score;	
	}

	public void SetWin ()
	{
		winScreen.SetActive(true);
		winScoreText.text = "<b>YOU ACHIEVED A SCORE OF</b>\n" + manager.score;
	}

	public void TryAgainButton ()
	{
		gameOverScreen.SetActive(false);
		winScreen.SetActive(false);
		manager.StartGame();
	}

	public void MenuButton ()
	{
		SceneManager.LoadScene(0);
	}
}

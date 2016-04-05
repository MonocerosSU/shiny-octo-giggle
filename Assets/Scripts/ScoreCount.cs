using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

	public Text highscore;
	public Text scoreText;
	public int score;
	public int highScore;
	string highScoreKey = "Highscore";


	void Start ()
	{
		score = 0;
		UpdateScore ();
		highScore = PlayerPrefs.GetInt(highScoreKey,0);    
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void Update ()
	{
		highscore.text = "Highscore:" + highScore;
		if (score > highScore) 
		{
			highscore.text = "Highscore: " + score ;
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
		highscore.text = "Highscore: " + highScore;
	}
	void OnDisable(){

		//If our scoree is greter than highscore, set new higscore and save.
		if(score>highScore)
		{
			PlayerPrefs.SetInt(highScoreKey, score);
			PlayerPrefs.Save();
		}
	}


}
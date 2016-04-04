using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

	public Text scoreText;
	private int score;

	void Start ()
	{
		score = 0;
		UpdateScore ();
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
}
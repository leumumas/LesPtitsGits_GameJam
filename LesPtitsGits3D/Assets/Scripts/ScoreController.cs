using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text currentScoreText;
	public Text HighScoreText;
	public Text HighScore2Text;
	public Text HighScore3Text;

	long currentScore;

	void Start()
	{
		currentScore = 0;
		currentScoreText.text = currentScore.ToString();
		UpdateHighScoreVisuals();
	}

	public void AddCasualtiesToScore(int casualties)
	{
		currentScore += casualties;
		currentScoreText.text = currentScore.ToString();

		if (CheckIfHighScoreWasBested())
		{
			UpdateHighScoreVisuals();
		}
	}

	private void UpdateHighScoreVisuals()
	{
		HighScoreText.text = PlayerPrefs.GetString("HighScore", "0");
		HighScore2Text.text = PlayerPrefs.GetString("HighScore2", "0");
		HighScore3Text.text = PlayerPrefs.GetString("HighScore3", "0");
	}

	private bool CheckIfHighScoreWasBested()
	{
		if (currentScore > long.Parse(PlayerPrefs.GetString("HighScore3")))
		{
			if (currentScore > long.Parse(PlayerPrefs.GetString("HighScore2")))
			{
				if (currentScore > long.Parse(PlayerPrefs.GetString("HighScore")))
				{
					PlayerPrefs.SetString("HighScore", currentScore.ToString());
					return true;
				}
				PlayerPrefs.SetString("HighScore2", currentScore.ToString());
				return true;
			}
			PlayerPrefs.SetString("HighScore3", currentScore.ToString());
			return true;
		}

		return false;
	}

	private void Update()
	{
		if (Input.GetKeyDown("r"))
		{
			//  CAREFULL WITH THIS, IT RESETS ALL HIGHSCORES
			Debug.Log("ScoreController::Update(): You've resetted all highscores saved");
			PlayerPrefs.DeleteAll();
		}
	}
}

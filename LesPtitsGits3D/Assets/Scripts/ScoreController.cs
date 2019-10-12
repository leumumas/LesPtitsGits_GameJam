using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text currentScoreText;
	public Text HighScoreText;
	public Text HighScore2Text;
	public Text HighScore3Text;

	int currentScore = 0;

	void Start()
	{
		currentScore = 0;
		currentScoreText.text = currentScore == 0 ? "" : currentScore.ToString();
		UpdateHighScoreVisuals();
	}

	public void AddCasualtiesToScore(int casualties)
	{
		currentScore += casualties;
		currentScoreText.text = currentScore.ToString();

		if (HighScoreWasBested())
		{
			UpdateHighScoreVisuals();
		}
	}

	private void UpdateHighScoreVisuals()
	{
		HighScoreText.text = PlayerPrefs.GetInt("HighScore", 0) == 0 ? "" : PlayerPrefs.GetInt("HighScore", 0).ToString();
		HighScore2Text.text = PlayerPrefs.GetInt("HighScore2", 0) == 0 ? "" : PlayerPrefs.GetInt("HighScore2", 0).ToString();
		HighScore3Text.text = PlayerPrefs.GetInt("HighScore3", 0) == 0 ? "" : PlayerPrefs.GetInt("HighScore3", 0).ToString();
	}

	private bool HighScoreWasBested()
	{
		if (currentScore > PlayerPrefs.GetInt("HighScore3"))
		{
			if (currentScore > PlayerPrefs.GetInt("HighScore2"))
			{
				if (currentScore > PlayerPrefs.GetInt("HighScore"))
				{
					PlayerPrefs.SetInt("HighScore", currentScore);
					return true;
				}
				PlayerPrefs.SetInt("HighScore2", currentScore);
				return true;
			}
			PlayerPrefs.SetInt("HighScore3", currentScore);
			return true;
		}

		return false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Click! + 10  casualties");
			AddCasualtiesToScore(1000);
		}

		if (Input.GetKeyDown("r"))
		{
			//  CAREFULL WITH THIS, IT RESETS ALL HIGHSCORES
			Debug.Log("ScoreController::Update(): You've resetted all highscores saved");
			PlayerPrefs.DeleteAll();
		}
	}
}

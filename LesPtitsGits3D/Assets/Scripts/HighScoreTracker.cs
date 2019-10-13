using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTracker : MonoBehaviour
{
	public static HighScoreTracker Instance { get; protected set; }
	public GameObject HighScoreTrackerGO;
	public GameObject HighScoreCanvas;
	public List<Text> HighscoreTexts;
	public Image NewHighScore;

	private List<string> HighScoreNames = new List<string>
	{
		"HighScore",
		"HighScore2",
		"HighScore3",
		"HighScore4",
		"HighScore5",
		"HighScore6",
		"HighScore7",
		"HighScore8",
		"HighScore9",
		"HighScore10"
	};

	private void OnEnable()
	{
		if (Instance != null)
		{
			Debug.LogError("HighScoreTracker: There should never more than one instance of HighScoreTracker");
		}

		Instance = this;

		DontDestroyOnLoad(HighScoreCanvas);
		DontDestroyOnLoad(HighScoreTrackerGO);
		UpdateHighScoreVisuals();
		NewHighScore.GetComponent<CanvasGroup>().alpha = 0.0f;
	}

	public void UpdateHighScoreVisuals()
	{
		foreach (Text textGO in HighscoreTexts)
		{
			string score = PlayerPrefs.GetString(textGO.name, "");
			textGO.text = score;
			if (score == "")
			{
				PlayerPrefs.SetString(textGO.name, "0");
			}
		}
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

	public void EndGameNewScore(long score)
	{
		HighScoreCanvas.SetActive(true);
		int highScoreIndex = CheckIfHighScoreWasBested(score);
		if (highScoreIndex != -1)
		{
			UpdateHighScoreOrder(highScoreIndex, score);
			UpdateHighScoreVisuals();
		}
	}

	public void GameStart()
	{
		HighScoreCanvas.SetActive(false);
	}

	public int CheckIfHighScoreWasBested(long gameScore)
	{
		int scoreIndex = 0;

		foreach (string score in HighScoreNames)
		{
			if (gameScore > long.Parse(PlayerPrefs.GetString(score)))
			{
				return scoreIndex;
			}
			scoreIndex++;
		}

		return -1;
	}

	private void UpdateHighScoreOrder(int highScoreIndex, long score)
	{
		for (int i = (HighScoreNames.Count - 1); i > (highScoreIndex - 1); i--)
		{
			if (i == highScoreIndex)
			{
				PlayerPrefs.SetString(HighScoreNames[i], score.ToString());
				NewHighScore.transform.position = new Vector3(NewHighScore.transform.position.x, HighscoreTexts[i].transform.position.y + 10, NewHighScore.transform.position.z);
				NewHighScore.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
			else
			{
				PlayerPrefs.SetString(HighScoreNames[i], PlayerPrefs.GetString(HighScoreNames[i - 1]));
			}
		}
	}
}

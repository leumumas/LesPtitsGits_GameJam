using System.Collections.Generic;
using UnityEngine;


public class HighScoreTracker : MonoBehaviour
{
	public static HighScoreTracker Instance { get; protected set; }
	public GameObject highScoreTrackerGO;

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
		"HighScore10",
	};

	private void OnEnable()
	{
		if (Instance != null)
		{
			Debug.LogError("HighScoreTracker: There should never more than one instance of HighScoreTracker");
		}

		Instance = this;

		DontDestroyOnLoad(highScoreTrackerGO);
	}

	private bool CheckIfHighScoreWasBested(long currentScore)
	{
		List<string> reversedHighscores = HighScoreNames;
		reversedHighscores.Reverse();

		foreach (string score in reversedHighscores)
		{
			if (currentScore > long.Parse(PlayerPrefs.GetString(score)))
			{
				return true;
			}
		}

		return false;
	}
}

using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public static ScoreController Instance { get; protected set; }
	public Text currentScoreText;

	long currentScore;

	private void OnEnable()
	{
		Instance = this;
	}

	void Start()
	{
		currentScore = 0;
		currentScoreText.text = currentScore.ToString();
	}

	public void AddCasualtiesToScore(int casualties)
	{
		currentScore += casualties;
		currentScoreText.text = currentScore.ToString();
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Click! add 10 000 to score.");
			AddCasualtiesToScore(10000);
		}
	}

	public void EndGame()
	{
		HighScoreTracker.Instance.EndGameNewScore(currentScore);
	}
}

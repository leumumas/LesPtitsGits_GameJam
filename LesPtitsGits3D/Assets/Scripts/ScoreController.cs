using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public static ScoreController Instance { get; protected set; }
	public Text currentScoreText;
	public Image NewHighScore;

	long currentScore;

	private void OnEnable()
	{
		Instance = this;

		NewHighScore.GetComponent<CanvasGroup>().alpha = 0.0f;
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

		if (HighScoreTracker.Instance.CheckIfHighScoreWasBested(currentScore) != -1)
		{
			NewHighScore.GetComponent<CanvasGroup>().alpha = 1.0f;
		}
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
		NewHighScore.GetComponent<CanvasGroup>().alpha = 0.0f;
	}
}

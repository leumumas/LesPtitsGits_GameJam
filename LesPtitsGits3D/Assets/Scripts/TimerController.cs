using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
	public Text TimerText;

	private int Year;
	private float InternalTimer;
	[SerializeField]
	private float NextYearDelay = 0.5f;

	private readonly static int NEW_YEAR_FONT_SIZE = 19;
	private readonly static int DEFAULT_FONT_SIZE = 14;

	private void Start()
	{
		InternalTimer = Time.deltaTime;
		Year = 2019;
		TimerText.text = Year.ToString();
	}

	private void Update()
	{
		InternalTimer += Time.deltaTime;

		if (InternalTimer > NextYearDelay)
		{
			Year++;
			UpdateTimerVisuals();
			InternalTimer = 0;
		}

		if (TimerText.fontSize != DEFAULT_FONT_SIZE)
		{
			TimerText.fontSize--;
		}

		if (Year >= 2039)
		{
			ScoreController.Instance.EndGame();
			SceneManager.LoadScene("TitleScreen");
		}
	}

	private void UpdateTimerVisuals()
	{
		TimerText.fontSize = NEW_YEAR_FONT_SIZE;
		TimerText.text = Year.ToString();
	}
} 

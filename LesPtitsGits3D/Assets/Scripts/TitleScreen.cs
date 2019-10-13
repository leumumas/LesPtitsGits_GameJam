using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	public GameObject Earth;

	private void Update()
	{
		Earth.transform.Rotate(0, 12.0f * Time.deltaTime, 0);

		if (Input.GetButtonDown("Submit"))
		{
			HighScoreTracker.Instance.GameStart();
			SceneManager.LoadScene("EarthVsHuman");
		}
	}
}
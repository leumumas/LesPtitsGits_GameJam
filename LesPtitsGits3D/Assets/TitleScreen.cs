using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
	public List<Text> HighscoreTexts;
	public GameObject Earth;
	
    private void OnEnable()
    {
		foreach (Text textGO in HighscoreTexts)
		{
			string score = PlayerPrefs.GetString(textGO.name, "0");
			textGO.text = score;
			if (score == "0")
			{
				PlayerPrefs.SetString(textGO.name, "0");
			}
		}
	}

	private void Update()
	{
		Earth.transform.Rotate(0, 12.0f * Time.deltaTime, 0);

		if (Input.GetButtonDown("Submit"))
		{
			SceneManager.LoadScene("EarthVsHuman");
		}
	}
}
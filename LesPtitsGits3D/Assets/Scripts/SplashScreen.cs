using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
	private void OnEnable()
	{

		SceneManager.LoadScene("TitleScreen");

	}
}

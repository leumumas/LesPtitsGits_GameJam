using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Material ButtonOnCooldown;
	public Material ButtonReady;
	public GameObject TornadoButton;
	public GameObject VolcanoButton;
	public GameObject EarthquakeButton;
	
	[SerializeField]
	private float TornadoCooldown = 2.5f;
	private float timeTornadoCooldown = 0f;
	private bool isTornadoOnCooldown = false;
	[SerializeField]
	private float VolcanoCooldown = 2.5f;
	private float timeVolcanoCooldown = 0f;
	private bool isVolcanoOnCooldown = false;
	[SerializeField]
	private float EarthquakeCooldown = 2.5f;
	private float timeEarthquakeCooldown = 0f;
	private bool isEarthquakeOnCooldown = false;

	private void Update()
	{
		// Button "Submit" is also Gamepad 0 which is the A button on a controller.
		if (Input.GetButtonDown("Submit") && !isTornadoOnCooldown)
		{
			// Just test stuff and add score when pressing the gamepad A button
			//Debug.Log("Click! + 1 000 000 000  casualties");
			//FindObjectOfType<ScoreController>().AddCasualtiesToScore(1000000000);

			// TODO match the Torando action with the A Button
			isTornadoOnCooldown = true;
			TornadoButton.GetComponent<Image>().material = ButtonOnCooldown;
		}

		if (Input.GetButtonDown("Cancel") && !isVolcanoOnCooldown)
		{
			isVolcanoOnCooldown = true;
			VolcanoButton.GetComponent<Image>().material = ButtonOnCooldown;
		}

		if (Input.GetButtonDown("Fire3") && !isEarthquakeOnCooldown)
		{
			isEarthquakeOnCooldown = true;
			EarthquakeButton.GetComponent<Image>().material = ButtonOnCooldown;
		}

		if (isTornadoOnCooldown)
		{
			timeTornadoCooldown += Time.deltaTime;
			TornadoButton.GetComponent<CanvasGroup>().alpha = (timeTornadoCooldown / TornadoCooldown);
		}

		if (isVolcanoOnCooldown)
		{
			timeVolcanoCooldown += Time.deltaTime;
			VolcanoButton.GetComponent<CanvasGroup>().alpha = (timeVolcanoCooldown / VolcanoCooldown);
		}

		if (isEarthquakeOnCooldown)
		{
			timeEarthquakeCooldown += Time.deltaTime;
			EarthquakeButton.GetComponent<CanvasGroup>().alpha = (timeEarthquakeCooldown / EarthquakeCooldown);
		}

		if (isTornadoOnCooldown && timeTornadoCooldown >= TornadoCooldown)
		{
			timeTornadoCooldown = 0f;
			isTornadoOnCooldown = false;
			TornadoButton.GetComponent<Image>().material = ButtonReady;
		}

		if (isVolcanoOnCooldown && timeVolcanoCooldown >= VolcanoCooldown)
		{
			timeVolcanoCooldown = 0f;
			isVolcanoOnCooldown = false;
			VolcanoButton.GetComponent<Image>().material = ButtonReady;
		}

		if (isEarthquakeOnCooldown && timeEarthquakeCooldown >= EarthquakeCooldown)
		{
			timeEarthquakeCooldown = 0f;
			isEarthquakeOnCooldown = false;
			EarthquakeButton.GetComponent<Image>().material = ButtonReady;
		}
	}
}

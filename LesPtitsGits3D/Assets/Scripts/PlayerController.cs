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
    private GameObject m_Tornado;
    [SerializeField]
    private GameObject EarthTransform;
    [SerializeField]
	private float VolcanoCooldown = 2.5f;
	private float timeVolcanoCooldown = 0f;
	private bool isVolcanoOnCooldown = false;
    [SerializeField]
    private GameObject m_Volcano;
    [SerializeField]
	private float EarthquakeCooldown = 2.5f;
	private float timeEarthquakeCooldown = 0f;
	private bool isEarthquakeOnCooldown = false;
    [SerializeField]
    private GameObject m_EarthQuake;

    private GameObject m_CurrentTornado;
    private Vector3 m_TornadoScale = new Vector3(4f, 4f, 4f);
    private Vector3 m_TornadoRotation = new Vector3(0f, 0f, 0f);

    private RaycastHit raycastHit;
    private Ray ray;

    private Camera MainCamera;

    private GlobalRegion m_LastRegion;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
	{
        // Button "Submit" is also Gamepad 0 which is the A button on a controller.

        bool canTornado = RegionHandler.Instance.CurrentGlobalRegion.Tornado;
        bool canEarthQuake = RegionHandler.Instance.CurrentGlobalRegion.EarthQuake;
        bool canVolcano = RegionHandler.Instance.CurrentGlobalRegion.Volcano;


        ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            GlobalRegion globalRegion = RegionHandler.Instance.CurrentGlobalRegion;
            if (globalRegion != null && globalRegion.Tornado)
            {
                if (Input.GetButtonDown("Submit") && !isTornadoOnCooldown)
                {
                    m_LastRegion = globalRegion;
                    SpawnTornado(raycastHit.point, raycastHit.normal);
                }
            }

            if (Input.GetButtonUp("Submit") && m_CurrentTornado != null)
            {
                isTornadoOnCooldown = true;
                Vector3 endPosition = raycastHit.point;
                bool shouldMove = true;
                if (globalRegion == null || !globalRegion.Tornado || m_LastRegion.NameRegion != globalRegion.NameRegion)
                {
                    shouldMove = false;
                }
                SetTornado(endPosition, raycastHit.normal, shouldMove);
                m_CurrentTornado = null;
            }

            if (Input.GetButtonDown("Cancel") && !isVolcanoOnCooldown && canVolcano)
            {
                isVolcanoOnCooldown = true;
                SpawnVolcano(raycastHit.point, raycastHit.normal);
            }

            if (Input.GetButtonDown("Fire3") && !isEarthquakeOnCooldown && canEarthQuake)
            {
                isEarthquakeOnCooldown = true;
                SpawnEarthQuake(raycastHit.point, raycastHit.normal);
            }
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
		}

		if (isVolcanoOnCooldown && timeVolcanoCooldown >= VolcanoCooldown)
		{
			timeVolcanoCooldown = 0f;
			isVolcanoOnCooldown = false;
		}

		if (isEarthquakeOnCooldown && timeEarthquakeCooldown >= EarthquakeCooldown)
		{
			timeEarthquakeCooldown = 0f;
			isEarthquakeOnCooldown = false;
        }
        TornadoButton.GetComponent<Image>().material = isTornadoOnCooldown || !canTornado ? ButtonOnCooldown : ButtonReady;
        EarthquakeButton.GetComponent<Image>().material = isEarthquakeOnCooldown || !canEarthQuake ? ButtonOnCooldown : ButtonReady;
        VolcanoButton.GetComponent<Image>().material = isVolcanoOnCooldown || !canVolcano ? ButtonOnCooldown : ButtonReady;
    }

    private void SpawnTornado(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        m_CurrentTornado = Instantiate(m_Tornado, i_HitPosition, Quaternion.FromToRotation(Vector3.up, i_HitNormal), EarthTransform.transform);
        m_CurrentTornado.transform.localScale = m_TornadoScale;
    }

    private void SetTornado(Vector3 i_HitPosition, Vector3 i_HitNormal, bool i_ShouldMove)
    {
        if (m_CurrentTornado == null)
        {
            return;
        }

        TornadoMovement tornadoMovement = m_CurrentTornado.GetComponent<TornadoMovement>();
        tornadoMovement.earth = EarthTransform.gameObject;
        tornadoMovement.SetEndPosition(Vector3.zero, i_HitPosition, i_ShouldMove, RegionHandler.Instance.CurrentGlobalRegion);
    }

    private void SpawnEarthQuake(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        EarthQuake earthQuake = Instantiate(m_EarthQuake, i_HitPosition, Quaternion.FromToRotation(Vector3.up, i_HitNormal), EarthTransform.transform).GetComponent<EarthQuake>();
        earthQuake.SetEarthQuake(RegionHandler.Instance.CurrentGlobalRegion);
    }

    private void SpawnVolcano(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        Volcano volcano = Instantiate(m_Volcano, i_HitPosition, Quaternion.FromToRotation(Vector3.up, i_HitNormal), EarthTransform.transform).GetComponent<Volcano>();
        volcano.SetVolcano(RegionHandler.Instance.CurrentGlobalRegion);
    }
}

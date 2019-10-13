using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAbility : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Tornado;

    [SerializeField][Tooltip("Cooldown of the ability in Seconds")]
    private float cooldownDuration = 3.0f;

    private float cooldownTime = -1.0f;

    private bool bInCooldown = false;

    private GameObject playerController;

    private GameObject m_CurrentTornado;
    private Vector3 m_TornadoScale = new Vector3(4f, 4f, 4f);
    private Vector3 m_TornadoRotation = new Vector3(0f, 0f, 0f);

    private RaycastHit raycastHit;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("PlayerController");
    }

    // Update is called once per frame
    void Update()
    {
        if (bInCooldown)
        {
            cooldownTime += Time.deltaTime;
            if (cooldownTime < cooldownDuration)
            {
                return;
            }
            cooldownTime = -1.0f;
            bInCooldown = false;
        }

        if(Input.GetButtonDown("SpawnTornado"))
        {
            Debug.Log(Input.mousePosition);
        }

        GlobalRegion globalRegion = RegionHandler.Instance.CurrentGlobalRegion;
        if (globalRegion != null && globalRegion.Tornado)
        {
            if (Input.GetButtonDown("SpawnTornado"))
            {
                SpawnTornado(raycastHit.point, raycastHit.normal);
            }
        }


        if (Input.GetButtonUp("SpawnTornado"))
        {
            SetTornado(raycastHit.point, raycastHit.normal);
        }
    }

    private void SpawnTornado(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        m_CurrentTornado = Instantiate(m_Tornado, i_HitPosition, Quaternion.FromToRotation(Vector3.up, i_HitNormal), this.transform);
        m_CurrentTornado.transform.localScale = m_TornadoScale;
    }

    private void SetTornado(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        if (m_CurrentTornado == null)
        {
            return;
        }

        TornadoMovement tornadoMovement = m_CurrentTornado.GetComponent<TornadoMovement>();
        tornadoMovement.earth = gameObject;
        //tornadoMovement.SetEndPosition(Vector3.zero, i_HitPosition, true, );
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Tornado;

    [SerializeField][Tooltip("Cooldown of the ability in Seconds")]
    private float cooldownDuration = 3.0f;

    private float cooldownTime = -1.0f;

    private bool bInCooldown = false;

    private GameObject playerController;

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
    }
}

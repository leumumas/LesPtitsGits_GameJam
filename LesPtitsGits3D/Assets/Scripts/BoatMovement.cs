using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private GameObject[] path;
    private Vector3 direction;
    private Transform myTransform;
    private int currentPathIndex = 0;

    private Vector3 lookAt;
    private Transform pathTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        pathTransform = path[currentPathIndex].GetComponent<Transform>();
        lookAt = direction.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(lookAt, myTransform.position - new Vector3(0, 0, 0));
        myTransform.rotation = targetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(lookAt * Time.deltaTime * 20f, Space.World);

        if((pathTransform.position - myTransform.position).magnitude < 2)
        {
            currentPathIndex++;

            if (currentPathIndex == path.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                pathTransform = path[currentPathIndex].GetComponent<Transform>();
                lookAt = pathTransform.position - myTransform.position;
                Quaternion targetRotation = Quaternion.LookRotation(lookAt, myTransform.position - new Vector3(0,0,0));
                lookAt = lookAt.normalized;
                myTransform.rotation = targetRotation;
            }
        }
    }

    public void SetPath(Vector3 direction, GameObject[] pathToUse)
    {
        this.path = pathToUse;
        this.direction = direction.normalized;
    }
}

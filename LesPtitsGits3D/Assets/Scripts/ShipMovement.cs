using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Vector3 endPosition;
    private Vector3 direction;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(direction * Time.deltaTime * 20f, Space.World);
        if((endPosition - myTransform.position).magnitude < 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetEndPosition(Vector3 direction, Vector3 endPosition)
    {
        this.endPosition = endPosition;
        this.direction = direction.normalized;
    }
}

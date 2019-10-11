using UnityEngine;
using System.Collections;

public class EarthSpinScript : MonoBehaviour
{
    public float speed = 10f;

    void Update() 
    {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);

        float translation = -Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * speed;

        transform.Rotate(Vector3.up, rotation * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, translation * Time.deltaTime, Space.World);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{

       [SerializeField]
       private float speed = 3f;
       [SerializeField]
       private float speedZoom = 1200f;
       [SerializeField]
       private float CloseValueCamera = 350f;
       [SerializeField]
       private float FarValueCamera = 800f;
       [SerializeField]
       private Transform Target;

       private float StartTime;
       private float journeyLength = 0f;
       private Vector3 StartPosition;
       private Vector3 EndPosition;

       private Vector3 FarPosition;
       private Vector3 ClosePosition;

       private bool m_IsZoomOut = true;

       // Start is called before the first frame update
       void Start()
       {
           FarPosition = new Vector3(0, 0, FarValueCamera);
           ClosePosition = new Vector3(0, 0, CloseValueCamera);
       }

       // Update is called once per frame
       void Update()
       {
           FarPosition = new Vector3(transform.position.x, transform.position.y, FarValueCamera);
           ClosePosition = new Vector3(transform.position.x, transform.position.y, CloseValueCamera);

           bool Change = false;
           if (Input.GetButtonDown("ZoomIn") && m_IsZoomOut)
           {
               StartTime = Time.time;
               journeyLength = Math.Abs(FarValueCamera - CloseValueCamera);
               StartPosition = transform.position;
               EndPosition = transform.position.normalized * CloseValueCamera;
               m_IsZoomOut = false;
                Target.GetComponentInParent<EarthControl>().SetIsZoomOut(m_IsZoomOut);
            }

           if (Input.GetButtonDown("ZoomOut") && !m_IsZoomOut)
           {
               StartTime = Time.time;
               journeyLength = Math.Abs(FarValueCamera - CloseValueCamera);
               StartPosition = transform.position;
               EndPosition = transform.position.normalized * FarValueCamera;
               m_IsZoomOut = true;
                Target.GetComponentInParent<EarthControl>().SetIsZoomOut(m_IsZoomOut);
            }

           float distCovered = (Time.time - StartTime) * speedZoom;

           float fractionOfJourney = distCovered / journeyLength;

           if (fractionOfJourney < 1f)
           {
               transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
           }
           else
           {
               transform.RotateAround(Target.position, transform.right, Input.GetAxis("Vertical") * speed);
               transform.RotateAround(Target.position, transform.up, -Input.GetAxis("Horizontal") * speed);
           }
       }
}

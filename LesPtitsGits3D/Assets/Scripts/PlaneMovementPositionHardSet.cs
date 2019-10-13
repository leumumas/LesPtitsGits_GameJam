using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneMovementPositionHardSet : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject endPositionObject;
    private Vector3 endPosition;
    private Vector3 direction;
    private Vector3 midPoint;
    private Transform myTransform;

    private GlobalRegion m_StartRegion;

    public GameObject earth;

    public GlobalRegion StartRegion => m_StartRegion;

    private Vector3[] points = new Vector3[3];
    private FlightPath[] flightPaths = new FlightPath[lineSteps];

    private const int lineSteps = 100;

    private int currentFlightIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        startPosition = myTransform.localPosition;

        midPoint = GetLineMidPoint();     
        midPoint = GetCurveMidPoint();
        
        points[0] = startPosition;
        points[1] = midPoint;
        points[2] = endPosition;
        Vector3 lineStart = GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++)
        {
            float currentT = i / (float)lineSteps;
            Vector3 lineEnd = GetPoint(currentT);
            flightPaths[i - 1].lineStart = lineStart;
            flightPaths[i - 1].lineEnd = lineEnd;
            flightPaths[i - 1].Rotation = new Vector3(0f,0f,0f);
            lineStart = lineEnd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentFlightStart = flightPaths[currentFlightIndex].lineStart;
        Vector3 currentFlightEnd = flightPaths[currentFlightIndex].lineEnd;
        Vector3 currentDirection = (currentFlightEnd - currentFlightStart).normalized;
        Quaternion currentRotation = Quaternion.LookRotation(currentDirection);

        currentRotation *= Quaternion.Euler(flightPaths[currentFlightIndex].Rotation);

        myTransform.rotation = currentRotation;
        myTransform.localPosition = currentFlightEnd;
        myTransform.Translate(currentDirection * Time.deltaTime * 20f, Space.World);

        if((currentFlightEnd - myTransform.localPosition).magnitude < 0.05 || (currentFlightEnd - myTransform.localPosition).magnitude > 0.1)
        {
            currentFlightIndex++;
        }

        if ((endPosition - myTransform.localPosition).magnitude < 10 / 200 || currentFlightIndex >= lineSteps)
        {
            Destroy(gameObject);
        }
    }

    Vector3 GetLineMidPoint()
    {
        return new Vector3((startPosition.x + endPosition.x)/2, (startPosition.y + endPosition.y) / 2, (startPosition.z + endPosition.z) / 2);
    }

    Vector3 GetPoint(float t)
    {
        return Vector3.Lerp(Vector3.Lerp(points[0], points[1], t), Vector3.Lerp(points[1], points[2], t), t);
    }

    Vector3 GetCurveMidPoint()
    {
        Vector3[] vectors = new Vector3[6];
        float[] magnitudes = new float[6];

        float test = (endPosition - startPosition).magnitude;
        float amplitude = 0.75f;
        
        if(test > 100 && test < 300)
        {
            amplitude = 0.65f;
        }
        else if (test < 100)
        {
            amplitude = 0.5f;
        }

        Vector3 midpointDirectionPosUp = Vector3.Cross(direction, endPositionObject.transform.up);
        vectors[0] = midPoint + midpointDirectionPosUp * amplitude;
        magnitudes[0] = (earth.transform.position - vectors[0]).magnitude;

        Vector3 midpointDirectionNegUp = Vector3.Cross(-direction, endPositionObject.transform.up);
        vectors[1] = midPoint + midpointDirectionNegUp * amplitude;
        magnitudes[1] = (earth.transform.position - vectors[1]).magnitude;

        Vector3 midpointDirectionFwd = Vector3.Cross(direction, endPositionObject.transform.forward);
        vectors[2] = midPoint + midpointDirectionFwd * amplitude;
        magnitudes[2] = (earth.transform.position - vectors[2]).magnitude;

        Vector3 midpointDirectionBwd = Vector3.Cross(-direction, endPositionObject.transform.forward);
        vectors[3] = midPoint + midpointDirectionBwd * amplitude;
        magnitudes[3] = (earth.transform.position - vectors[3]).magnitude;

        Vector3 midpointDirectionLeft = Vector3.Cross(-direction, endPositionObject.transform.right);
        vectors[4] = midPoint + midpointDirectionLeft * amplitude;
        magnitudes[4] = (earth.transform.position - vectors[4]).magnitude;

        Vector3 midpointDirectionRight = Vector3.Cross(direction, endPositionObject.transform.right);
        vectors[5] = midPoint + midpointDirectionLeft * amplitude;
        magnitudes[5] = (earth.transform.position - vectors[5]).magnitude;

        int highestIndex = 0;
        for(int i=1; i < magnitudes.Length; i++)
        {
            if(magnitudes[i] > magnitudes[highestIndex])
            {
                highestIndex = i;
            }
        }

        return vectors[highestIndex];
    }

    public void SetEndPosition(Vector3 direction, GameObject endPositionObject, GlobalRegion StartRegion)
    {
        this.endPositionObject = endPositionObject;
        this.endPosition = endPositionObject.transform.localPosition;
        this.direction = direction;
    }
}

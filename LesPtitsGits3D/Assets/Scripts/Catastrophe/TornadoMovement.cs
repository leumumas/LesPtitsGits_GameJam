using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPosition;
    private GameObject endPositionObject;
    private Vector3 endPosition;
    private Vector3 direction;
    private Vector3 midPoint;
    private Transform myTransform;

    public GameObject earth;

    private Vector3[] points = new Vector3[3];
    private FlightPath[] flightPaths;

    private const int baseLineSteps = 100;
    private int lineSteps = 0;

    private int currentFlightIndex = 0;

    private float duration = 5;

    private float timeBegin = 0;

    private bool m_StartMoving = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (myTransform == null || flightPaths.Length < 0)
        {
            return;
        }
        Vector3 currentFlightStart = flightPaths[currentFlightIndex].lineStart;
        Vector3 currentFlightEnd = flightPaths[currentFlightIndex].lineEnd;
        Vector3 currentDirection = (currentFlightEnd - currentFlightStart).normalized;
        Quaternion currentRotation = Quaternion.LookRotation(currentDirection);

        currentRotation *= Quaternion.Euler(-flightPaths[currentFlightIndex].Rotation);

        myTransform.localRotation = Quaternion.FromToRotation(Vector3.up, currentFlightEnd);
        myTransform.localPosition = currentFlightEnd;

        currentFlightIndex = (int)(lineSteps * (timeBegin / duration));

        timeBegin += Time.deltaTime;

        if (timeBegin > duration)
        {
            Destroy(gameObject);
        }
    }

    Vector3 GetPoint(float t)
    {
        return Vector3.Lerp(Vector3.Lerp(points[0], points[1], t), Vector3.Lerp(points[1], points[2], t), t);
    }

    Vector3 GetLineMidPoint()
    {
        return new Vector3((startPosition.x + endPosition.x) / 2, (startPosition.y + endPosition.y) / 2, (startPosition.z + endPosition.z) / 2);
    }

    Vector3 GetCurveMidPoint()
    {
        return midPoint.normalized;
    }

    public void SetEndPosition(Vector3 direction, Vector3 endPosition, bool i_ShouldMove)
    {
        myTransform = GetComponent<Transform>();
        /*Vector3 baseEulerAngle = (earth.transform.localEulerAngles * (Mathf.PI / 180)).normalized;
        Vector3 basePosition = earth.transform.InverseTransformPoint(endPosition).normalized;
        Vector3 newPosition = endPosition + baseEulerAngle;*/
        startPosition = myTransform.localPosition;
        Vector3 newPosition = i_ShouldMove ? (Quaternion.Inverse(earth.transform.localRotation) * endPosition).normalized : startPosition;  //endPosition * earth.transform.rotation;
        this.endPosition = newPosition;
        this.direction = direction;

        float distance = Vector3.Distance(this.endPosition, startPosition);

        lineSteps = (int)(baseLineSteps * (distance * 2));

        if (lineSteps < baseLineSteps)
        {
            lineSteps = baseLineSteps;
        }

        flightPaths = new FlightPath[lineSteps];

        midPoint = GetLineMidPoint();
        midPoint = GetCurveMidPoint();

        points[0] = startPosition;
        points[1] = midPoint;
        points[2] = this.endPosition;
        Vector3 lineStart = GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++)
        {
            float currentT = i / (float)lineSteps;
            Vector3 lineEnd = GetPoint(currentT);
            flightPaths[i - 1].lineStart = lineStart;
            flightPaths[i - 1].lineEnd = lineEnd;
            flightPaths[i - 1].Rotation = Quaternion.FromToRotation(Vector3.down, lineStart).eulerAngles;
            lineStart = lineEnd;
        }

        m_StartMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlaneMovement planeMovement = other.GetComponent<PlaneMovement>();
        BoatMovement boatMovement = other.GetComponent<BoatMovement>();
        if (planeMovement != null)
        {
            GameManager.Instance.RemoveGlobalRegionPopulation(planeMovement.RegionSpawn.m_NameRegion, GameManager.Instance.PlaneScore);
            Destroy(planeMovement.gameObject);
        }
        if (boatMovement != null)
        {
            GameManager.Instance.RemoveGlobalRegionPopulation("Asia", GameManager.Instance.PlaneScore);
            Destroy(boatMovement.gameObject);
        }
    }
}

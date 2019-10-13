using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnScript : MonoBehaviour
{
    private float spawnTimer;
    private float checkSpawn = 1f;
    private GameObject[] boatAiLocations;
    private GameObject[] planeAiLocations;
    private GameObject[] carAiLocations;
    private System.Random rnd = new System.Random();

    public GameObject boatPrefab;
    public GameObject planePrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Time.deltaTime;

        boatAiLocations = GameObject.FindGameObjectsWithTag("BoatLocation");
        planeAiLocations = GameObject.FindGameObjectsWithTag("PlaneLocation");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > checkSpawn)
        {
            SpawnMayhem();
            spawnTimer = 0f;
        }
    }

    void SpawnMayhem()
    {
        int nbBoatsToSpawn = rnd.Next(1, 2);
        int nbPlanesToSpawn = rnd.Next(1, 2);
        int nbCarsToSpawn = rnd.Next(0, 2);


        SpawnBoats(nbBoatsToSpawn);
        SpawnPlanes(nbPlanesToSpawn);
    }

    void SpawnBoats(int numberToSpawn)
    {
       /* if(boatAiLocations.Length < numberToSpawn)
        {
            return;
        }

        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject PortToStartAt = boatAiLocations[rnd.Next(0, boatAiLocations.Length)];

            GameObject[] pathToTake = PortToStartAt.GetComponent<PortPaths>().GetPath();

            Vector3 lookAt = pathToTake[0].transform.position - PortToStartAt.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt);
            
            GameObject newBoat = Instantiate(boatPrefab, PortToStartAt.transform.position, targetRotation);
            newBoat.GetComponent<BoatMovement>().SetPath(lookAt, pathToTake);
        }*/
    }

    void SpawnPlanes(int numberToSpawn)
    {
        if (planeAiLocations.Length < numberToSpawn)
        {
            return;
        }
        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject startPosition = planeAiLocations[rnd.Next(0, planeAiLocations.Length)];
            GameObject endPosition;

            do
            {
                endPosition = planeAiLocations[rnd.Next(0, planeAiLocations.Length)];
            } while (startPosition == endPosition);

            Vector3 lookAt = endPosition.transform.position - startPosition.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt);

            GameObject newPlane = Instantiate(planePrefab, startPosition.transform.position, targetRotation);
            newPlane.GetComponent<PlaneMovement>().SetEndPosition(lookAt, endPosition, startPosition.GetComponent<SpawnLocationInfo>().spawnRegionLocation);
        }
    }
}

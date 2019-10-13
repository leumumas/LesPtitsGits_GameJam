using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortPaths : MonoBehaviour
{
    public PathsArray[] AllPaths;
    private System.Random rnd = new System.Random();
    public GameObject[] GetPath()
    {
        GameObject[] pathToTake = AllPaths[rnd.Next(0, AllPaths.Length)].Path;
        return pathToTake;
    }
}

[System.Serializable]
public class PathsArray
{
    public GameObject[] Path;
}

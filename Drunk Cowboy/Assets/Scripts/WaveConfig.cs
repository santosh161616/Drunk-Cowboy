using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bottle Waves")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject bottlePrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float spawnRandomFactor;
    [SerializeField] int numberOfBottles = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetBottlePrefab()
    {
        return bottlePrefab;
    }

    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float SpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int NumberOfBottles()
    {
        return numberOfBottles;
    }

    public float MoveSpeed()
    {
        return moveSpeed;
    }
}

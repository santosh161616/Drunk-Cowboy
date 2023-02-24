using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int waveIndex = 0;
    public int bottlesLeftInWave;
    Gun gunReference;

    float timeRemaining = 3f;

    // Start is called before the first frame update
    void Start()
    {      
        gunReference = FindObjectOfType<Gun>();
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {        
        var currentWave = waveConfigs[waveIndex];
        bottlesLeftInWave = waveConfigs[waveIndex].NumberOfBottles();
        yield return StartCoroutine(SpawnAllBottlesInWave(currentWave));        
        waveIndex++;
        if (waveIndex >= waveConfigs.Count)
        {
            waveIndex = 0;
        }
    }
    
    private IEnumerator SpawnAllBottlesInWave(WaveConfig waveConfig)
    {
        for (int bottleCount = 0; bottleCount < waveConfig.NumberOfBottles(); bottleCount++)
        {
            var newBottle = Instantiate(waveConfig.GetBottlePrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newBottle.GetComponent<Pathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());           
        }
    }

    private void Update()
    {
        if(bottlesLeftInWave == 0)
        {
            StartCoroutine(SpawnAllWaves());
        }
        else
        {
            if(gunReference.GetMissedShots() > 3)
            {
                if (timeRemaining > 3f)
                {
                    timeRemaining -= Time.deltaTime;
                    int seconds = Mathf.CeilToInt(timeRemaining);
                    Debug.Log(seconds);
                }
            }
        }
    }
}

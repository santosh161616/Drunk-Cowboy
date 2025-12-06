using cowboy.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;

    int waveIndex = 0;
    public int _bottlesLeftInWave;
    private Coroutine _waveStartCoroutine;
    public void BottlesLeftInWave(int value)
    {
        _bottlesLeftInWave -= value;
        if(_bottlesLeftInWave <= 0)
        {
            if(_waveStartCoroutine != null)
            {
                StopCoroutine(_waveStartCoroutine);
            }
            _waveStartCoroutine = StartCoroutine(SpawnAllWaves());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
        GameEvents.Instance.OnBottleLeft += BottlesLeftInWave;
    }

    private IEnumerator SpawnAllWaves()
    {
        var currentWave = waveConfigs[waveIndex];
        _bottlesLeftInWave = waveConfigs[waveIndex].NumberOfBottles();
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

    private void OnDestroy()
    {
        GameEvents.Instance.OnBottleLeft -= BottlesLeftInWave;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{   
    public AudioClip bottleDestroySFX;
    [Range(0, 1)]
    public float bottleDestroyVolume;
    BottlesSpawner bottlesSpawner;
    int count = 1;    

    Gun gun;

    private void Start()
    {
        bottlesSpawner = FindObjectOfType<BottlesSpawner>();
        gun = FindObjectOfType<Gun>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bottlesSpawner.bottlesLeftInWave--;
        gun.BrokenBottles(count);
        gun.CountHitShots(true, count);
        AudioSource.PlayClipAtPoint(bottleDestroySFX, Camera.main.transform.position, 1f);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}

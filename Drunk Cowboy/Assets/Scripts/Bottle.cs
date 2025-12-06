using cowboy.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{   
    public AudioClip bottleDestroySFX;
    [Range(0, 1)]
    public float bottleDestroyVolume;
    
    int count = 1;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.Instance.BottleLeft(count);
        GameEvents.Instance.BottleBroken(count);
        GameEvents.Instance.CountingHitShots(true, count);
        AudioSource.PlayClipAtPoint(bottleDestroySFX, Camera.main.transform.position, 1f);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}

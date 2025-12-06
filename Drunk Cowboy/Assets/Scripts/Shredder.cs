using cowboy.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    int count = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Gun"))
        {
            //gun.CountHitShots(false, count);
            GameEvents.Instance.CountingHitShots(false, count);
        }
        Destroy(collision.gameObject);
    }
}

using cowboy.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    int _value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Gun"))
        {
            GameEvents.Instance.CountingHitShots(false, _value);
        }
        Destroy(collision.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    Gun gun;
    int count = 1;
    private void Start()
    {
        gun = FindObjectOfType<Gun>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gun.CountHitShots(false, count);
        Destroy(collision.gameObject);
    }
}

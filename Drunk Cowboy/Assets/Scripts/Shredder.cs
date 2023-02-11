using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    Gun gun;
    private void Start()
    {
        gun = FindObjectOfType<Gun>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gun.CountHitShots(false, 0);
        Destroy(collision.gameObject);
    }
}

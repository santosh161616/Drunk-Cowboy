using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManager : MonoBehaviour
{
    public int activeBottles = 0;
    public GameObject[] bottles;
    // Start is called before the first frame update
    void Start()
    {
        bottles = GameObject.FindGameObjectsWithTag("Bottle");
        foreach (GameObject bottle in bottles)
        {
            if (bottle.activeSelf)
            {
                activeBottles++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        activeBottles = 0;
        foreach (GameObject bottle in bottles)
        {
            if (bottle.activeSelf)
            {
                activeBottles++;
            }
        }
        
    }
}

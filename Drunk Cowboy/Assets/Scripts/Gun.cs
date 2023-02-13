using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{    
    public int speed = 5;
    public GameObject projectile;
    public GameObject shotPoint;
    public AudioClip gunShotSFX;
    [Range(0,1)]
    public float gunShotVolume; 
    public float timeBerweenShots = 0.2f;

    public int score = 0;    
    float shotTime;
    public int bulletsLeft = 8;
    int countShots = 0;
    bool hitTarget;

    public Image[] bullets;
    public Sprite loadedBullets;
    public Sprite emptyBullets;

    // Update is called once per frame
    void Update()
    {
        UpdateLoadBulletsUI(bulletsLeft);
        transform.Rotate(0, 0, speed * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime && bulletsLeft > 0)
            {
               /* hitTarget = false;
                CountHitShots(hitTarget, countShots);*/

                AudioSource.PlayClipAtPoint(gunShotSFX, Camera.main.transform.position, gunShotVolume);
                Instantiate(projectile, shotPoint.transform.position, transform.rotation);
                shotTime = timeBerweenShots + Time.time;
                bulletsLeft--;
                if(bulletsLeft <= 0)
                {
                    bulletsLeft = 8;
                }
            }            
        }
    }

    public void BrokenBottles(int newbBokenBottles)
    {
        score += newbBokenBottles;
    }

    public int GetScore()
    {
        return score;
    }

    //Function to load and unload bullet Sprites
    public void UpdateLoadBulletsUI(int bulletsLeft)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if(i < bulletsLeft)
            {
                bullets[i].sprite = loadedBullets;
            }
            else
            {
                bullets[i].sprite = emptyBullets;
            }
        }
    }

    public void CountHitShots(bool isHitTarget, int clearShot)
    {
        if (isHitTarget)
        {
            countShots += clearShot;
            if (countShots >= 1)
            {
                FindObjectOfType<PopupText>().GenerateRandomWord();
            }
        }
        else
        {
            countShots = 0;
        }
        Debug.Log(countShots + ";" + isHitTarget);
    }
}

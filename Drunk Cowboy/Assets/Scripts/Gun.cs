using cowboy.utils;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int speed = 6;
    public GameObject projectile;               //Bullets projectile.    
    public GameObject shotPoint;                //Shotpoint to shoot bullets.
    public GameObject timerObj;                 //Object from Timer Class
    public AudioClip gunShotSFX;                //Sound effect for bullets.
    [Range(0, 1)]
    public float gunShotVolume;                 //Volume for Gun sound. 
    public float timeBerweenShots = 0.2f;       //time between bullet shots.
    private bool _isGamePlayble = true;

    public int score = 0;
    float shotTime;
    public int bulletsLeft = 8;
    int countShots = 0;
    public int missedShots = 0;

    public Image[] bullets;                     //Array to hold Bullets
    public Sprite loadedBullets;                //Loaded Bullet sprites.
    public Sprite emptyBullets;                 //Empty bullet sprites.

    int tempValue = 3;                          // Integer to validate the bullet stoper.   

    public bool IsGamePlayble { get { return _isGamePlayble; } set { _isGamePlayble = value; } }


    private void Start()
    {
        GameEvents.Instance.OnBottleBreak += BrokenBottles;
        GameEvents.Instance.OnCountingHitShots += CountHitShots;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLoadBulletsUI(bulletsLeft);
        if (_isGamePlayble)
        {
            PlayGame();
        }
        else
        {
            GameStopper();
        }
        // transform.Rotate(0, 0, speed * Time.deltaTime);
        if (Input.GetMouseButton(0) && missedShots < tempValue)
        {
            if (Time.time >= shotTime && bulletsLeft > 0)
            {
                /* hitTarget = false;
                 CountHitShots(hitTarget, countShots);*/

                AudioSource.PlayClipAtPoint(gunShotSFX, Camera.main.transform.position, gunShotVolume);
                Instantiate(projectile, shotPoint.transform.position, transform.rotation);
                shotTime = timeBerweenShots + Time.time;
                bulletsLeft--;
                if (bulletsLeft <= 0)
                {
                    bulletsLeft = 8;
                }
            }
        }
        //if (missedShots >= tempValue /* Add a bool from Timer to validate */)
        //{
        //    timerObj.GetComponent<Timer>().StartTimer(true);
        //}
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
            if (i < bulletsLeft)
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
            missedShots = 0;
            if (countShots >= tempValue)
            {
                FindObjectOfType<PopupText>().DisplayRandomText();
            }
        }
        else
        {
            countShots = 0;
            missedShots += clearShot;
            if(missedShots >= tempValue)
            {
                GameEvents.Instance.MissedEnoughShots();
                GameEvents.Instance.TimerStart(true);
                _isGamePlayble = false;
            }          
            
        }
        Debug.Log("Count Shots" + countShots + ";" + isHitTarget);
        Debug.Log("Missed Shots" + missedShots + ";" + isHitTarget);
    }

    public int GetMissedShots()
    {
        return missedShots;
    }

    public void DisableTimer()
    {
        IsGamePlayble = true;
        timerObj.SetActive(false);
    }

    public void EnableTimer()
    {

        timerObj.SetActive(true);
    }

    public void GameStopper()
    {
        transform.Rotate(Vector3.zero);
    }

    public void PlayGame()
    {
        transform.Rotate(0, 0, -speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnBottleBreak -= BrokenBottles;
        GameEvents.Instance.OnCountingHitShots -= CountHitShots;
    }
}

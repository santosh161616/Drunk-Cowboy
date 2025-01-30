using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] private List<Transform> wayPoints;
    int wayPointIndex = 0;
    Action OnReachingwaypoint;
    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    float targetRotationZ = 0f;
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, -270 * Time.deltaTime);
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var moveThisFrame = waveConfig.MoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetRotationZ -= 90f;

                // Apply rotation directly (without RotateTowards)
                transform.rotation = Quaternion.Euler(0, 0, targetRotationZ);
                Debug.Log("Rotating -" + transform.rotation);
                wayPointIndex++;
                if (wayPointIndex == wayPoints.Count)
                {
                    wayPointIndex = 0;
                }
            }
            //if(transform.position == targetPosition)
            //{                
            //    wayPointIndex++;
            //    if(wayPointIndex < 3)
            //    {
            //        anim.SetBool("idle", true);
            //        anim.SetBool("isFacingDown", false);
            //        anim.SetBool("isFacingRight", false);
            //    }
            //    if(wayPointIndex >= 3)
            //    {
            //        anim.SetBool("isFacingRight", true);
            //        anim.SetBool("isFacingDown", false);
            //        anim.SetBool("idle", false);
            //    }
            //    if(wayPointIndex >=5 && wayPointIndex < 7)
            //    {
            //        anim.SetBool("isFacingDown", true);
            //        anim.SetBool("idle", false);
            //        anim.SetBool("isFacingRight", false);
            //    }
            //    if(wayPointIndex == wayPoints.Count)
            //    {                    
            //        wayPointIndex = 0;
            //    }
            //}
        }
    }
}

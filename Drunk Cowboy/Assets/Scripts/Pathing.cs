using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPointIndex <= wayPoints.Count-1)
        {           
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var moveThisFrame = waveConfig.MoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);
            if(transform.position == targetPosition)
            {                
                wayPointIndex++;
                if(wayPointIndex < 3)
                {
                    anim.SetBool("idle", true);
                    anim.SetBool("isFacingDown", false);
                    anim.SetBool("isFacingRight", false);
                }
                if(wayPointIndex >= 3)
                {
                    anim.SetBool("isFacingRight", true);
                    anim.SetBool("isFacingDown", false);
                    anim.SetBool("idle", false);
                }
                if(wayPointIndex >=5 && wayPointIndex < 7)
                {
                    anim.SetBool("isFacingDown", true);
                    anim.SetBool("idle", false);
                    anim.SetBool("isFacingRight", false);
                }
                if(wayPointIndex == wayPoints.Count)
                {                    
                    wayPointIndex = 0;
                }
            }
        }
    }    
}

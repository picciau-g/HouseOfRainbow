using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBackAndForth : ActorBase
{
    [SerializeField]
    Transform[] wayPoints;

    int currentWayPointsIndex;


    // Start is called before the first frame update
    void Start()
    {
        currentWayPointsIndex = 0;
        isRunning = false;
        moveSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPoints.Length > 1)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPointsIndex].position, step);

            //if it gets to the wayopint, move it to the next one
            if(Vector3.Distance(transform.position, wayPoints[currentWayPointsIndex].position) < 0.05f)
            {
                currentWayPointsIndex = (currentWayPointsIndex + 1) % wayPoints.Length;
            }
        }

        processKeyboardInput();
    }
}

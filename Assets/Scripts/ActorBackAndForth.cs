using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBackAndForth : ActorBase
{
    [SerializeField]
    Transform[] wayPoints;

    int currentWayPointsIndex;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        patrollingEnemyState.wayPoints = wayPoints;
        currentWayPointsIndex = 0;
        isRunning = false;
        moveSpeed = 2.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

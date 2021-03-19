using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    protected float moveSpeed;
    protected bool isRunning;
    [SerializeField]
    protected int maxSightDistance;
    [SerializeField]
    protected double viewAngle;
    [SerializeField]
    protected EnemyStateMachine _mEnemySM;
    [SerializeField]
    protected Transform[] wayPoints;

    public PatrollingEnemyState patrollingEnemyState;
    public ChasingEnemyState chasingEnemyState;
    public AttackingEnemyState attackingEnemyState;


    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }



    protected virtual void Start()
    {
        _mEnemySM = new EnemyStateMachine();

        //initialize States with new
        patrollingEnemyState = new PatrollingEnemyState(this, _mEnemySM);
        chasingEnemyState = new ChasingEnemyState(this, _mEnemySM);
        attackingEnemyState = new AttackingEnemyState(this, _mEnemySM);

        if (patrollingEnemyState == null)
            Debug.Log("NULL ENEMY STATE");
        else
            Debug.Log("ALRIGHT, ENEMY NOT NULL");

        //Initialize the Enemy state
        _mEnemySM.Initialize(patrollingEnemyState);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _mEnemySM.CurrentState.Update();
        _mEnemySM.CurrentState.LogicUpdate();
    }


   

    protected void WalkToRun()
    {
        isRunning = !isRunning;

        if (isRunning)
            moveSpeed = 4.0f;
        else
            moveSpeed = 2.0f;
    }
}

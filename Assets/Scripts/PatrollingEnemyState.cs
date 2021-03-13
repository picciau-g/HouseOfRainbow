using UnityEngine;
using System.Collections;

public class PatrollingEnemyState : BaseEnemyState
{

    float currentTime;
    float patrolSpeed;
    [SerializeField]
    float rotationSpeed = 2.0f;
    [SerializeField]
    int rotationDeltaTime = 5;

    public Transform[] wayPoints;

    int currentWayPointsIndex;

    public PatrollingEnemyState(ActorBase pActor, EnemyStateMachine pEnemySM)
        :
        base(pActor, pEnemySM)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Patrolling");
        currentTime = 0.0f;
        patrolSpeed = 2.0f;
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        if (_playerInSight)
            _mEnemySM.ChangeState(_mEnemyAssociated.chasingEnemyState);
    }

    public override void Update()
    {
        //Move
        MoveEnemy();

        //Do we see the player?
        if (CheckOnPlayer())
            _playerInSight = true;
        else
            _playerInSight = false;
    }


    public override void MoveEnemy()
    {
        if (wayPoints.Length > 1)
        {
            float step = patrolSpeed * Time.deltaTime;
            _mEnemyAssociated.transform.position = Vector3.MoveTowards(_mEnemyAssociated.transform.position, wayPoints[currentWayPointsIndex].position, step);

            //if it gets to the wayopint, move it to the next one
            if (Vector3.Distance(_mEnemyAssociated.transform.position, wayPoints[currentWayPointsIndex].position) < 0.05f)
            {
                currentWayPointsIndex = (currentWayPointsIndex + 1) % wayPoints.Length;
            }
        }
    }

}

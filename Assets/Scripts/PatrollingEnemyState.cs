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

        wayPoints = _mEnemyAssociated.GetWayPoints();
        _mEnemyAssociated.transform.position = wayPoints[0].position;

        if(wayPoints.Length > 1)
        {
            Vector3 direction = (wayPoints[1].position - wayPoints[0].position).normalized;
            float rotAngle = Vector3.Angle(_mEnemyAssociated.transform.forward, direction);
            _mEnemyAssociated.transform.Rotate(Vector3.up, -rotAngle);
        }

    }

    public override void Exit()
    {
        
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

        LineRenderer lineRenderer = _mEnemyAssociated.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, _mEnemyAssociated.transform.position);
        lineRenderer.SetPosition(1, _mEnemyAssociated.transform.forward * 20 + _mEnemyAssociated.transform.position);

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

                Vector3 dst2CheckPoint = (wayPoints[currentWayPointsIndex].transform.position - _mEnemyAssociated.transform.position).normalized;

                float rotAngle = Vector3.Angle(_mEnemyAssociated.transform.forward, dst2CheckPoint);
                //_mEnemyAssociated.transform.Rotate(Vector3.up, -rotAngle);
                _mEnemyAssociated.StartCoroutine(RotateActor(Vector3.up, rotAngle));

            }
        }
    }

    IEnumerator RotateActor(Vector3 pAxis, float pAngle, float pDuration = 1.0f)
    {

        Quaternion from = _mEnemyAssociated.transform.rotation;
        Quaternion to = _mEnemyAssociated.transform.rotation;

        to *= Quaternion.Euler(pAxis * pAngle);

        float elapsed = 0.0f;

        while (elapsed < pDuration)
        {
            _mEnemyAssociated.transform.rotation = Quaternion.Slerp(from, to, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _mEnemyAssociated.transform.rotation = to;

        // currentArrayIndex = (currentArrayIndex+1)%4;
    }
}

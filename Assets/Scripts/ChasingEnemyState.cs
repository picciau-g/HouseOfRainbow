using UnityEngine;
using System.Collections;

public class ChasingEnemyState : BaseEnemyState
{
    float _chaseSpeed;

    public ChasingEnemyState(ActorBase pActor, EnemyStateMachine pEnemySM)
        :
        base(pActor, pEnemySM)
    {

    }


    public override void EnterState()
    {
        Debug.Log("Chasing Player");
        _chaseSpeed = 3.0f;
    }

    public override void LogicUpdate()
    {
        if (!_playerInSight)
            _mEnemySM.ChangeState(_mEnemyAssociated.patrollingEnemyState);
        //else if (_playerWithinDistance)
          //  _mEnemySM.ChangeState(_mEnemyAssociated.attackingEnemyState);
    }

    public override void MoveEnemy()
    {
        Vector3 direction = _playerToChase.transform.position - _mEnemyAssociated.transform.position;
        //Vector3.MoveTowards(_mEnemyAssociated.transform.position, _playerToChase.transform.position, _chaseSpeed * Time.deltaTime);
        _mEnemyAssociated.transform.Translate(direction.x * _chaseSpeed * Time.deltaTime, 0, direction.z * _chaseSpeed * Time.deltaTime);
    }

    public override void Update()
    {
        MoveEnemy();


        if (!CheckOnPlayer())
            _playerInSight = false;
        else
        {
            _playerInSight = true;
            if (CheckPlayerDistance())
                _playerWithinDistance = true;
            else
                _playerWithinDistance = false;
        }
    }
}

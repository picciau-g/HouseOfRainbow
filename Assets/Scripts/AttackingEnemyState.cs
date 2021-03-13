using UnityEngine;
using System.Collections;

public class AttackingEnemyState : BaseEnemyState
{

    float _attackSpeed;
    float _damageAmount;

    public AttackingEnemyState(ActorBase pActor, EnemyStateMachine pEnemySM)
        :
        base(pActor, pEnemySM)
    {

    }

    public override void EnterState()
    {
        //
        _attackSpeed = 4.0f;
    }

    public override void LogicUpdate()
    {
        if(!_playerWithinDistance)
        {
            if (_playerInSight)
                _mEnemySM.ChangeState(_mEnemyAssociated.chasingEnemyState);
            else
                _mEnemySM.ChangeState(_mEnemyAssociated.patrollingEnemyState);
        }
    }

    public override void MoveEnemy()
    {
        //throw new System.NotImplementedException();
        //Animate the swing

        //subtract points from the player

        

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

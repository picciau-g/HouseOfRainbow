using UnityEngine;

public abstract class BaseEnemyState
{
    protected float _speed = 2.0f;

    //The player character
    protected GameObject _playerToChase;

    protected bool _playerInSight;
    protected bool _playerWithinDistance;

    public virtual void SetInSight(bool pInSight) { _playerInSight = pInSight; }
    public virtual void SetWithinDistance(bool pWithinDistance) { _playerWithinDistance = pWithinDistance; }

    //FSM stuff. A reference to the enemy and one to the State Machine governing it
    protected ActorBase _mEnemyAssociated;
    protected EnemyStateMachine _mEnemySM;

    protected BaseEnemyState(ActorBase pEnemy, EnemyStateMachine pEnemySM)
    {
        this._mEnemyAssociated = pEnemy;
        this._mEnemySM = pEnemySM;


        _playerToChase = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual bool CheckOnPlayer()
    {
        if (_playerToChase == null)
        {
            Debug.Log("NO PLAYER HERE");
            return false;
        }

        Vector3 distToPlayer = (_playerToChase.transform.position - _mEnemyAssociated.transform.position).normalized;
        float angle = Vector3.Angle(_mEnemyAssociated.transform.forward, distToPlayer);


        if (angle > 45)
            return false;

        LayerMask msk = LayerMask.GetMask("PlayerLayer");

        if (Physics.Raycast(_mEnemyAssociated.transform.position, distToPlayer, 30, msk))
        {
            return true;
        }

        return false;
    }

    protected virtual bool CheckPlayerDistance()
    {
        if (!_playerInSight)
            return false;

        float dst = Vector3.Distance(_playerToChase.transform.position, _mEnemyAssociated.transform.position);

        if (dst < 15)
            return true;

        return false;
    }

    public abstract void EnterState();

    public abstract void Update();

    public abstract void LogicUpdate();

    public abstract void MoveEnemy();

    public virtual void Exit() { }

}

using UnityEngine;
using System.Collections;

public class EnemyStateMachine 
{

    public BaseEnemyState CurrentState { get; private set; }


    public void Initialize(BaseEnemyState pStartingState)
    {
        CurrentState = pStartingState;
        pStartingState.EnterState();
    }


    public void ChangeState(BaseEnemyState pNewState)
    {
        CurrentState.Exit();
        CurrentState = pNewState;
        pNewState.EnterState();
    }

}

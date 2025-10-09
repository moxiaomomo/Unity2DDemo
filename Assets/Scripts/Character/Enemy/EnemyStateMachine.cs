using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState _startingState)
    {
        currentState = _startingState;
        currentState.Enter();
    }

    public bool ChangeState(EnemyState _newState)
    {
        if (currentState == _newState) 
        {
            return false;
        }
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
        return true;
    }

}

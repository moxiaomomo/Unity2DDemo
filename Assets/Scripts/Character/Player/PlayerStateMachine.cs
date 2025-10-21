using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        if (currentState == _newState)
            return;
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }


    public void ChangeState(PlayerState _newState, string enterStateName)
    {
        if (currentState == _newState && enterStateName==currentState.stateName)
            return;
        currentState.Exit();
        currentState = _newState;
        currentState.Enter(enterStateName);
    }

    public bool NeedChangeState(PlayerState _newState, string enterStateName)
    {
        return currentState != _newState || enterStateName != currentState.stateName;
    }
}

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
        //// 通知Pet对象Player状态已改变
        //PetOfPlayer.playerStateChanged.Invoke(currentState.stateName);
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
        // 通知Pet对象Player状态已改变
        PetOfPlayer.playerStateChanged.Invoke(currentState.stateName);
    }

}

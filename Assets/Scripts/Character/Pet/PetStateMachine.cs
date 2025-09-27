using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

public class PetStateMachine
{
    public PetState currentState { get; private set; }

    public void Initialize(PetState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(PetState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

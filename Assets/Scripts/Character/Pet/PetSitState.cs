using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PetSitState : PetState
{
    public PetSitState(PetOfPlayer _pet, PetStateMachine _stateMachine, string _stateName) : base(_pet, _stateMachine, _stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {  
        base.Exit(); 
    }
}

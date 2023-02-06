using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Resume();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (owner.perceived.Length == 0)
        {
            owner.stateMachine.StartState(nameof(IdleState));
        }
        else
        {
            owner.movement.MoveTowards(owner.perceived[0].transform.position);

            Vector3 direction = owner.perceived[0].transform.position - owner.transform.position;
            float distance = direction.magnitude;
            float angle = Vector3.Angle(owner.transform.forward, direction);

            if (distance < 2.5 && angle < 20)
            {
                owner.stateMachine.StartState(nameof(AttackState));
            }
        }
    }
}

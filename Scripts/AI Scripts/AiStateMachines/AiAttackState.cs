using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    RaycastHit raycastHit;

    public void Enter(AiAgent agent)
    {
        agent.animator.SetBool("attack", true);
        agent.navMeshAgent.velocity = Vector3.zero;
        agent.navMeshAgent.isStopped = true;
    }

    public void Exit(AiAgent agent)
    {
        agent.animator.SetBool("attack", false);
        agent.navMeshAgent.isStopped = false;
    }

    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }

    public void Update(AiAgent agent)
    {

        if (agent.agentPlayerDistance > agent.attackDistance + 5)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}

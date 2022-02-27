using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public Vector3 direction;
    RaycastHit hitInfo;

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = agent.agent_speed;

        agent.weaponAim.weight = 1;

    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {

        //agent.transform.LookAt(agent.playerTransform.position);


        // if (agent.agentPlayerDistance > chaseDistance)
        // {
        //     agent.stateMachine.ChangeState(AiStateId.Find);
        // }
        if (agent.agentPlayerDistance > agent.attackDistance)
        {
            agent.navMeshAgent.SetDestination(agent.playerTransform.position);
        }
        else if (agent.agentPlayerDistance < agent.attackDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.Attack);
        }

    }
}

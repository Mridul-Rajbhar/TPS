using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindState : AiState
{
    public float movement = 10f;
    public Vector3 tempDestination;
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 1;
        agent.navMeshAgent.stoppingDistance = 1f;
        agent.weaponAim.weight = 0;
        tempDestination = agent.transform.position;
        //tempDestination = new Vector3(5 + agent.transform.position.x, 0, agent.);
        //Debug.Log(tempDestination);

    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.attackDistance;
    }

    public AiStateId GetId()
    {

        return AiStateId.Find;
    }

    Vector3 GetNewDestination(ref AiAgent agent)
    {
        Vector3 direction = agent.transform.position + new Vector3(Random.Range(-movement, movement), 0f, Random.Range(-movement, movement));
        checkWallColliison(agent);
        return direction;
    }

    bool CheckEnemyLook(Vector3 playerDirection, AiAgent agent)
    {
        playerDirection.Normalize();
        Vector3 agentDirection = agent.transform.forward;
        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            return true;
        }
        else
        {
            return false;
        }
    }

    void FindNewDestination(AiAgent agent)
    {
        float distance = Vector3.Distance(agent.transform.position, tempDestination);

        //        Debug.Log("distance: " + distance + " tempDistance: " + tempDestination);
        if (distance < 5f || checkWallColliison(agent))
        {
            //Debug.Log("distance: " + distance);
            tempDestination = GetNewDestination(ref agent);
        }
    }

    bool checkWallColliison(AiAgent agent)
    {
        int environment = LayerMask.NameToLayer("Environment");

        Ray Forward = new Ray(agent.transform.position, agent.transform.forward);
        Ray Right = new Ray(agent.transform.position, agent.transform.right);
        Ray Left = new Ray(agent.transform.position, -agent.transform.right);

        if (Physics.Raycast(Forward, 4f, environment) || Physics.Raycast(Left, 4f, environment) || Physics.Raycast(Right, 4f, environment))
            return true;
        else
            return false;
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        Debug.DrawRay(agent.transform.position, agent.transform.forward * 4f);
        Debug.DrawRay(agent.transform.position, agent.transform.right * 4f);
        Debug.DrawRay(agent.transform.position, -agent.transform.right * 4f);

        //Debug.Log("Player Direction: " + playerDirection);
        agent.navMeshAgent.SetDestination(tempDestination);
        agent.tempTarget = tempDestination;
        //Debug.Log("agent destination: " + agent.navMeshAgent.destination);


        if (agent.agentPlayerDistance > agent.chaseDistance)
        {
            FindNewDestination(agent);

            //Debug.Log("Speed: " + agent.animator.GetFloat("Speed"));
            return;
        }

        if (!CheckEnemyLook(playerDirection, agent))
        {
            FindNewDestination(agent);
        }



    }
}

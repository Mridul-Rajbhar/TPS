using UnityEngine;
public class AiDeathState : AiState
{
    public Vector3 direction;
    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        direction.y = 1;
        agent.ragdoll.ApplyForce(direction * agent.aiAgentConfig.Deathforce);
        agent.navMeshAgent.enabled = false;
        agent.uIHealthScript.gameObject.SetActive(false);
    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Update(AiAgent agent)
    {
        GameObject.Destroy(agent.gameObject, 3f);
    }
}

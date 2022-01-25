using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    
    Animator animator;

    NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();   
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // agent.destination = playerTransform.position; //Triggers a path plan every frame so we need to limit it.#   
       animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AiAgent : MonoBehaviour
{
    public AiSetTarget aiSetTarget;
    public Rig weaponAim;
    public Transform playerTransform;
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public UIHealthScript uIHealthScript;

    public NavMeshAgent navMeshAgent;
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public AiAgentConfig aiAgentConfig;

    public Animator animator;
    public RaycastWeapon raycastWeapon;

    public float attackDistance, agentPlayerDistance;
    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        raycastWeapon = GetComponentInChildren<RaycastWeapon>();
        aiSetTarget = GetComponent<AiSetTarget>();
        animator = GetComponent<Animator>();
        uIHealthScript = GetComponentInChildren<UIHealthScript>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ragdoll = GetComponent<Ragdoll>();
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        //stateMachine.RegisterState(new AiFindState());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.ChangeState(initialState);

        animator.Play(raycastWeapon.gameObject.name, 0);
        aiSetTarget.gunAim = raycastWeapon.rayCastOrigin;


    }

    // Update is called once per frame
    void Update()
    {
        agentPlayerDistance = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log(stateMachine.currentState);
        stateMachine.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AiAgent : MonoBehaviour
{
    public Sound sound;
    public AudioSource audioSource;
    public AiSetTarget aiSetTarget;
    public Rig weaponAim;
    public Transform playerTransform;
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public UIHealthScript uIHealthScript;

    public NavMeshAgent navMeshAgent;
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    //public AiAgentConfig aiAgentConfig;

    public Animator animator;
    public RaycastWeapon raycastWeapon;

    public GameObject healthPickUp, ak47, pistol;
    public float attackDistance, agentPlayerDistance, chaseDistance, Deathforce;

    public Vector3 offset;
    public float agent_speed;

    public Vector3 tempTarget;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<Sound>();
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

    public void pickups()
    {
        int n = Random.Range(0, 6);
        if (n >= 4)
            Instantiate(healthPickUp, transform.position + offset, Quaternion.identity);
        else if (n >= 1)
        {
            Instantiate(ak47, transform.position + offset, Quaternion.identity);
        }
        else
        {
            Instantiate(pistol, transform.position + offset, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        agentPlayerDistance = Vector3.Distance(transform.position, playerTransform.position);

        stateMachine.Update();
        //Debug.Log(stateMachine.currentState);
    }
}

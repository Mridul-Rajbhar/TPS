using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShoot : MonoBehaviour
{
    public RaycastWeapon raycastWeapon;
    public AiSetTarget aiSetTarget;
    public AiAgent aiAgent;

    // Start is called before the first frame update
    void Start()
    {
        raycastWeapon = GetComponent<RaycastWeapon>();
        aiSetTarget = GetComponentInParent<AiSetTarget>();
        aiAgent = GetComponentInParent<AiAgent>();
        aiAgent.attackDistance = raycastWeapon.range;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiAgent.stateMachine.currentState == AiStateId.Attack && aiSetTarget.insight)
            raycastWeapon.StartFiring(aiSetTarget.target.position);
    }
}

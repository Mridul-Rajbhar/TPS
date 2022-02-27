using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShoot : MonoBehaviour
{
    public RaycastWeapon raycastWeapon;
    public AiSetTarget aiSetTarget;
    public AiAgent aiAgent;

    CharacterMovement playerCharacterMovement;
    // Start is called before the first frame update
    void Start()
    {
        raycastWeapon = GetComponent<RaycastWeapon>();
        aiSetTarget = GetComponentInParent<AiSetTarget>();
        aiAgent = GetComponentInParent<AiAgent>();
        aiAgent.attackDistance = raycastWeapon.range;
        playerCharacterMovement = aiAgent.playerTransform.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiAgent.stateMachine.currentState == AiStateId.Attack && aiSetTarget.insight && playerCharacterMovement.currentHealth > 0)
        {
            // Debug.Log("Shooting");
            raycastWeapon.StartFiring(aiSetTarget.target.position);
            // if (raycastWeapon.weaponName == "handgun")
            // {
            //     aiAgent.sound.play_pistol_shoot(aiAgent.audioSource);
            // }
            // else
            // {
            //     aiAgent.sound.play_riffle_sound(aiAgent.audioSource);
            // }
        }
    }
}

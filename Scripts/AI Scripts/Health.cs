using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject gun;
    bool dead = false;
    AiAgent aiAgent;
    UIHealthScript uIHealthScript;
    public float maxHealth;
    SkinnedMeshRenderer skinnedMeshRenderer;
    [HideInInspector]
    public float currentHealth;
    Ragdoll ragdoll;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        uIHealthScript = GetComponentInChildren<UIHealthScript>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ragdoll = GetComponent<Ragdoll>();
        aiAgent = GetComponent<AiAgent>();

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        uIHealthScript.SetHealthBar(currentHealth / maxHealth);
        //Debug.Log(currentHealth/maxHealth);
        if (currentHealth <= 0.0f && !dead)
        {
            dead = true;
            Die(direction);
        }
        blinkTimer = blinkDuration;

    }

    private void Die(Vector3 direction)
    {
        AiDeathState deathState = aiAgent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        aiAgent.playerTransform.GetComponent<CharacterMovement>().score += 10;
        //DropGun();

        aiAgent.stateMachine.ChangeState(AiStateId.Death);
    }

    private void DropGun()
    {
        gun.transform.SetParent(null);

        if (gun.GetComponent<BoxCollider>().enabled == false)
        {
            gun.AddComponent<Rigidbody>();
            gun.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            gun.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void Update()
    {

        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration); //blending  factor
        float intensity = (lerp * blinkIntensity) + 1f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}

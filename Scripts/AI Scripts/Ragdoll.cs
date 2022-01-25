using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody[] rigidbodies;
    Animator animator;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        animator.enabled = false;
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public  void DeactivateRagdoll()
    {
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidbody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}

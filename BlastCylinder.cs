using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCylinder : MonoBehaviour
{
    [SerializeField]
    GameObject cylinder, broken_cylinder;
    [SerializeField]
    ParticleSystem fireExplosion;
    [SerializeField]
    SphereCollider sphereCollider;
    bool blasted = false;

    public int health;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        cylinder.SetActive(true);
        broken_cylinder.SetActive(false);
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !blasted)
        {
            Blast();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.transform.root.gameObject;
        if (temp.CompareTag("enemy"))
        {
            Debug.Log("enemy");
            Vector3 direction = transform.position - other.transform.position;
            temp.GetComponent<Health>().TakeDamage(25, direction);
        }
        else if (temp.CompareTag("Player"))
        {
            Vector3 direction = transform.position - other.transform.position;
            temp.GetComponent<CharacterMovement>().TakeDamage(25, direction);
        }

        var cylinder = temp.GetComponent<BlastCylinder>();
        if (cylinder)
        {
            Vector3 direction = transform.position - other.transform.position;
            cylinder.TakeDamage(25);
        }
    }

    void EnableSphereCollider()
    {
        sphereCollider.enabled = false;
    }

    void Blast()
    {
        blasted = !blasted;

        cylinder.SetActive(false);
        fireExplosion.Play();
        broken_cylinder.SetActive(true);
        sphereCollider.enabled = true;
        Invoke("EnableSphereCollider", 1);
    }
}

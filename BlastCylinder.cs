using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCylinder : MonoBehaviour, IDamageable
{
    public GameObject cylinder, broken_cylinder;
    [SerializeField]
    ParticleSystem fireExplosion;
    [SerializeField]
    SphereCollider sphereCollider;

    public GameManager gameManager;

    [HideInInspector]
    public bool blasted = false;

    public int health;
    [SerializeField]
    bool right = true;
    public float currentHealth, destination;

    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;

        currentHealth = health;
        cylinder.SetActive(true);
        broken_cylinder.SetActive(false);
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !blasted)
        {
            Blast();
        }
    }

    void MoveCylinder()
    {

        if (Vector3.Distance(currentPosition, transform.position) < 1)
        {
            right = true;
        }
        else if (Vector3.Distance(transform.position, currentPosition) > 19)
        {
            right = false;
        }

        if (Vector3.Distance(currentPosition, transform.position) < 20 && right)
        {
            right = true;
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, currentPosition) > 1 && !right)
        {
            right = false;
            transform.Translate(Vector3.right * Time.deltaTime * -1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        MoveCylinder();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.transform.root.gameObject;

        IDamageable DamageableObject = temp.GetComponent<IDamageable>();
        if (DamageableObject != null)
        {
            Vector3 direction = transform.position - other.transform.position;
            DamageableObject.TakeDamage(25, direction);
        }

        var enemyHealth = temp.GetComponent<Health>();
        if (enemyHealth)
        {
            Vector3 direction = transform.position - other.transform.position;
            enemyHealth.TakeDamage(25, direction);
        }


    }

    void EnableSphereCollider()
    {
        sphereCollider.enabled = false;
    }

    void Blast()
    {
        blasted = !blasted;
        //Debug.Log("blasted");
        //gameManager.blastedCylinder.Add(gameObject);

        fireExplosion.Play();
        cylinder.SetActive(false);
        broken_cylinder.SetActive(true);
        sphereCollider.enabled = true;
        Invoke("EnableSphereCollider", 1);
    }
}

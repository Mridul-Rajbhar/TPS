using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMotion : MonoBehaviour
{
    #region Private Variables

    public float Health, speed;
    public float currentHealth;
    [HideInInspector]
    bool isAiming;
    Vector3 velocity;

    static public bool isSprinting;

    CharacterController character_controller;
    #endregion


    #region Public Variables
    public float gravity, jump_height, air_control;
    public bool canJump;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        character_controller = GetComponent<CharacterController>();
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");

        canJump = character_controller.isGrounded;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            velocity.y = jump_height;
        }

        if (!canJump)
        {
            velocity.y -= gravity * Time.deltaTime;
            character_controller.Move(CalculateAirControl() * Time.deltaTime);
        }
        character_controller.Move(velocity * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Debug.Log("death");
        }
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * velocity.y) + (transform.right * velocity.x)) * air_control;
    }

}

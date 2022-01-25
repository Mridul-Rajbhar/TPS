
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    #region Private Variables

    public float Health;
    public float currentHealth;
    Animator animator;
    [HideInInspector]
    public Vector2 input;
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
        animator = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        canJump = character_controller.isGrounded;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        animator.SetBool("isSprinting", isSprinting);
        if (canJump)
        {
            animator.SetFloat("x", input.x);
            animator.SetFloat("y", input.y);
        }

        animator.SetBool("isAiming", Input.GetKey(KeyCode.Mouse1));

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            velocity.y = jump_height;
            animator.SetTrigger("jumped");
        }

        if (!canJump)
        {
            velocity.y -= gravity * Time.deltaTime;
            character_controller.Move(CalculateAirControl() * Time.deltaTime);
        }
        character_controller.Move(velocity * Time.deltaTime);
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
        return ((transform.forward * input.y) + (transform.right * input.x)) * air_control;
    }


}

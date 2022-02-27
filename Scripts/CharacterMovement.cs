using System.Collections;
using UnityEngine;
using TMPro;
public class CharacterMovement : MonoBehaviour, IDamageable
{

    #region Private Variables

    ActiveWeapon activeWeapon;

    Animator animator;
    [HideInInspector]
    public Vector2 input;
    bool isAiming;
    Vector3 velocity;

    static public bool isSprinting;

    CharacterController character_controller;
    #endregion


    #region Public Variables

    public Sprint sprint;
    public TMP_Text score_text;
    public UIHealthScript uIHealthScript;
    public float Health = 100;
    public float currentHealth;
    public float gravity, jump_height, air_control;
    public bool canJump;
    public int score = 0;

    [SerializeField]
    GameObject InGameUI, RestartScreen;
    CameraManager cameraManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = GetComponent<ActiveWeapon>();
        animator = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        currentHealth = Health;
        cameraManager = GameObject.FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + score;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        canJump = character_controller.isGrounded;
        uIHealthScript.SetHealthBar(currentHealth / Health);
        if (Input.GetKey(KeyCode.LeftShift) && ((sprint.current_stamina > 0 && !sprint.increasing) || (sprint.current_stamina > 2 && sprint.increasing)))
        {
            isSprinting = true;

        }
        else //if (Input.GetKey(KeyCode.LeftShift))
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

    public void TakeDamage(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        uIHealthScript.SetHealthBar(currentHealth / Health);
        // hitEffect.transform.position = hitInfo.point;
        // hitEffect.transform.forward = hitInfo.normal;
        // hitEffect.Emit(1);
        if (currentHealth <= 0)
        {
            StartCoroutine(GameOver());
            //Debug.Log("death");
        }
    }

    public IEnumerator GameOver()
    {
        animator.SetBool("killed", true);
        animator.SetLayerWeight(1, 0);
        cameraManager.EnableKillCam();
        GetComponent<CharacterAiming>().enabled = false;
        this.enabled = false;
        DropWeapon();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2f);

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        InGameUI.SetActive(false);
        RestartScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    void DropWeapon()
    {
        if (activeWeapon.active_weapon_index != -1)
        {

            GameObject gun = activeWeapon.equiped_weapon[activeWeapon.active_weapon_index].gameObject;
            Rigidbody rb = gun.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = true;
            gun.GetComponent<BoxCollider>().enabled = true;
            activeWeapon.DropWeapon();

        }
    }


    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * air_control;
    }


}

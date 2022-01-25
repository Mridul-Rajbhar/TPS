using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class CharacterAiming : MonoBehaviour
{
    #region Public Variables

    public float turnSpeed;
    public float aimDuration = 0.3f;
    public Transform camera_look_at;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    CharacterMovement characterMovement;



    #endregion

    #region Private Variables
    Camera mainCamera;



    #endregion


    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        camera_look_at.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        //Debug.Log(yAxis.Value + " " + xAxis.Value);
        //float yCamera = mainCamera.transform.rotation.eulerAngles.y;
        float camera_look_at_y = camera_look_at.rotation.eulerAngles.y;
        float camera_look_at_x = camera_look_at.rotation.eulerAngles.x;

        // if (characterMovement.input.x == 0 && characterMovement.input.y == 0 && !Input.GetKey(KeyCode.Mouse0))
        //     return;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, camera_look_at_y, 0), turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, camera_look_at_y, 0);
        camera_look_at.transform.localEulerAngles = new Vector3(camera_look_at_x, 0, 0);

    }


}

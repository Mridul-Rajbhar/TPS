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
    
    

    #endregion

    #region Private Variables
    Camera mainCamera;
    


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
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
        float yCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, yCamera, 0) , turnSpeed*Time.deltaTime);

    }


}

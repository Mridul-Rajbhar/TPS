
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables

    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    #endregion 


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.direction = mainCamera.transform.forward;
        ray.origin = mainCamera.transform.position;

        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green, 0.05f);
        if(Physics.Raycast(ray, out hitInfo))
            transform.position = hitInfo.point;  

    }
}

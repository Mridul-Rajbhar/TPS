using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera killcam;

    public void EnableKillCam()
    {
        killcam.Priority = 20;
    }
}

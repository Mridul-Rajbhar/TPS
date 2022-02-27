using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRaycast : MonoBehaviour
{
    public Transform raycastOrigin;

    private void Update() {
        //Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.forward * 100);
    }
}

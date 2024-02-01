using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Start()
    {
        Camera mainCamera = Camera.main;
        if(mainCamera == null) return;
        Quaternion cameraRot = mainCamera.transform.rotation;
 
        transform.LookAt(transform.position + cameraRot * Vector3.back,
            cameraRot * Vector3.up);
        
        transform.Rotate(Vector3.up, 180);

    }
}

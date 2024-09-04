using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform Cam;

   
    private void Update()
    {
        transform.position = Cam.position;
    }
}

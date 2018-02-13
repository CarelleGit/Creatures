using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 10f;
    float vertLookRotation;
    float horzRotation;

    public Transform cameraT;


    private void Start()
    {
        Cursor.visible = false;
        //For Clamps
        //Get the start rotations
        // add -60/60 for new clamps
        
    }

    // Update is called once per frame
    void Update()
    {
        var camera = Camera.main.transform;
        vertLookRotation += Input.GetAxis("Mouse Y") * -sensitivity;
        vertLookRotation = Mathf.Clamp(vertLookRotation, -90, 90);

        horzRotation += Input.GetAxis("Mouse X") * sensitivity;

        cameraT.localEulerAngles = new Vector3(vertLookRotation, horzRotation, 0);

      
    }
}

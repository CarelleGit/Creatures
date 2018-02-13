using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("e"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}

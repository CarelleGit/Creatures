using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek : MonoBehaviour {
    Vector3 desired;
    public float speed;
    public Transform target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 returnttargetspos()
    {
        return target.position;
    }
}

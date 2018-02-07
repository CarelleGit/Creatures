using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wander : MonoBehaviour {
    public float dis;
    public float radius;
    public float speed;
    public float jitter;
    public Vector3 target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 wandercontol()
    {
        target = Vector3.zero;
        target = Random.insideUnitCircle.normalized * radius;
        target = (Vector2)target + Random.insideUnitCircle * jitter;

        target.z = target.y;
        target += transform.position;
        target += transform.forward * dis;
        target.y = 0;
        return target;

    }
}

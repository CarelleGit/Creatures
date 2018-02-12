using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class offset_Pursuit : MonoBehaviour {
    public Transform leader;
    Vector3 offset;
    NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        offset = transform.position - leader.transform.position ;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        offsetpursuit();
        Debug.DrawLine(transform.position, agent.destination, Color.blue);
    }
    public void offsetpursuit()
    {
        Vector3 des = leader.transform.position + offset;
        agent.destination = des;
        
    }
}

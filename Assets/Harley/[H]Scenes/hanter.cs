using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum States
{
    wandermap,
    seek,
    flee,
}
public class hanter : MonoBehaviour {
    NavMeshAgent agent;
    public States state;
    public float radius;
    wander wander;
    seek seek;
    flee flee;
    public float hunger;
   public GameObject pray;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        seek = GetComponent<seek>();
        wander = GetComponent<wander>();
        flee = GetComponent<flee>();
	}
    bool pathComplete()
    {
        if (!agent.pathPending)
        {
            if(agent.stoppingDistance>= agent.remainingDistance)
            {
                if(!agent.hasPath||agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
        // Update is called once per frame
        void Update () {
        switch(state)
        {
            case States.wandermap:
                agent.destination = wander.wandercontol();
                break;
            case States.seek:
                agent.destination = seek.returnttargetspos();
                break;
            case States.flee:
                agent.destination = flee.returnFleeVector();
                break;
        }
        if(state == States.wandermap)
        {
            swichstate();
        }
       
	}
    void swichstate()
    {
        if(hunger <= 0)
        {
           
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider hit in hitColliders)
        {
            
            if (hit.tag == "pray")
            {
                state = States.seek;
                seek.target = hit.transform;
            }
            else if (hit.tag == "panditer")
            {

                state = States.flee;
                flee.target = hit.transform;

            }
            
           
        }
      
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "pray")
        {
            state = States.wandermap;
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}

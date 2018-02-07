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
public class hanter : MonoBehaviour,IDamageable
{
    NavMeshAgent agent;
    public States state;
    public float radius;
    wander wander;
    seek seek;
    flee flee;
    public float hunger;
   public GameObject pray;
    public float health;
    public float currenthealth;
    public hunterspawn spawn;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        seek = GetComponent<seek>();
        wander = GetComponent<wander>();
        flee = GetComponent<flee>();
        health = currenthealth;
 
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
                if (seek.target == null)
                {
                    state = States.wandermap;
                }
                agent.destination = seek.returnttargetspos();
                if (Vector3.Distance(transform.position, seek.target.position) <= 3)
                {
                    seek.target.GetComponent<IDamageable>().takeDamage(3);

                }
               
                break;
            case States.flee:
                agent.destination = flee.returnFleeVector();
                if (Vector3.Distance(flee.target.transform.position, transform.position) >= 15)
                {
                    state = States.wandermap;
                }
                break;
        }
        if(state == States.wandermap)
        {
            swichstate();
        }
        Debug.DrawLine(transform.position, agent.destination, Color.red);

    }
    void swichstate()
    {
        if(hunger <= 0)
        {
           
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider hit in hitColliders)
        {
            
            if (hit.tag == "Prey")
            {
                state = States.seek;
                seek.target = hit.transform;
            }
            else if (hit.tag == "Preditor")
            {

                state = States.flee;
                flee.target = hit.transform;

            }
            
           
        }
      
        
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        if(health<= 0)
        {
            die();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "prey")
        {
            state = States.wandermap;
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
   public void die()
    {
        
        spawn.currentanmontofenemys -= 1;
        Destroy(gameObject);
    }
}

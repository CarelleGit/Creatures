using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum preditor
{
    wander,
    chase
}

public class Preditor : MonoBehaviour, IDamageable
{
    wander wander;
    seek seek;

    public preditor creature;

    public float radius;

    public GameObject prey;
    public GameObject hunter;

    public float maxHealth;
    public float currentHealth;
    public float timer;
    public float docileTime;

    NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<wander>();
        seek = GetComponent<seek>();

        prey = GameObject.FindGameObjectWithTag("Prey");

        currentHealth = maxHealth;
    }
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
    }
    void switchControl()
    {
        if(creature == preditor.wander)
        {
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in hitCollider)
            {

                if (hit.tag == "Prey")
                {
                    seek.target = hit.transform;
                    creature = preditor.chase;
                }
                else if (hit.tag == "Hunter")
                {
                    seek.target = hit.transform;
                    creature = preditor.chase;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        switch (creature)
        {
            case preditor.wander:
                agent.destination = wander.wandercontol();
                agent.speed = 3;
                break;
            case preditor.chase:
                if (seek.target == null)
                {
                    creature = preditor.wander;
                }
                agent.destination = seek.returnttargetspos();
                if(Vector3.Distance(transform.position, seek.target.position) <= 3)
                {
                    seek.target.GetComponent<IDamageable>().takeDamage(3);
                    
                }
                agent.speed = 8;
                break;
        }
        switchControl();
        Debug.DrawLine(transform.position, agent.destination, Color.red);
    }
  
}

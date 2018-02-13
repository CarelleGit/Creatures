using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum preditor
{
    wander,
    chase,
    trap
}

public class Preditor : MonoBehaviour
{
    wander wander;
    seek seek;

    public preditor creature;

    public float radius;
    public float timerSet;
    public GameObject prey;
    public GameObject hunter;
    GameObject food;

    public float maxHealth;
    public float currentHealth;
    public float timer;
    public Spawner spawn;
    //public float docileTime;
    //public float setTime;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<wander>();
        seek = GetComponent<seek>();

        prey = GameObject.FindGameObjectWithTag("Prey");
        food = GameObject.FindGameObjectWithTag("Food");

        currentHealth = maxHealth;
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
                if (hit.tag == "Food")
                {
                    food = hit.gameObject;
                    //creature = preditor.trap;
                }
            }
            timer += Time.deltaTime;
            if (timer >= timerSet)
            {
                creature = preditor.trap;
                timer = 0;
            }
        }
        if(creature == preditor.trap)
        {
            timer += Time.deltaTime;
            if (timer >= timerSet)
            {
                creature = preditor.wander;
                timer = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        switch (creature)
        {
            case preditor.wander:
               // timer += Time.deltaTime;
                agent.destination = wander.wandercontol();
                agent.speed = 3;
               
                break;
            case preditor.chase:
                timer = 0;
                if (seek.target == null)
                {
                    creature = preditor.wander;
                }
                agent.destination = seek.returnttargetspos();
                if (Vector3.Distance(transform.position, seek.target.position) <= 3)
                {
                    seek.target.GetComponent<IDamageable>().takeDamage(3);
                }
                if (Vector3.Distance(transform.position, seek.target.position) > radius)
                {
                    creature = preditor.wander;
                }
                agent.speed = 8;
                break;
            case preditor.trap:
                agent.destination = food.transform.position;
              break;
        }
        switchControl();
        Debug.DrawLine(transform.position, agent.destination, Color.red);
    }
  
}

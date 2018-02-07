using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum prey
{
    Eat, Wander, Flee, die
}

public class Prey : MonoBehaviour, IDamageable
{
    Wander wander;
    Flee flee;
    Seek seek;

    public float currentHunger;
    public float maxHunger;
    public float health;
    public float maxHealth;
    public float radius;
    public prey currentState;

    public GameObject food;
    public GameObject hunter;
    public GameObject predetor;
    public float docileTime;//Time until the animal is docile again
    float timer;



    NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        timer = docileTime;

        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<Wander>();
        flee = GetComponent<Flee>();
        seek = GetComponent<Seek>();

        hunter = GameObject.FindGameObjectWithTag("Hunter");
        predetor = GameObject.FindGameObjectWithTag("Preditor");
        food = GameObject.FindGameObjectWithTag("Food");

        currentHunger = maxHunger;
        health = maxHealth;
    }
    void switchControl()
    {
        if (currentHunger <= 0)
        {
            currentState = prey.Eat;
        }

        if (currentState != prey.Flee)
        {
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in hitCollider)
            {
                if (hit.tag == "Preditor")
                {
                    flee.target = hit.transform;
                    currentState = prey.Flee;
                }
                if (hit.tag == "Hunter")
                {
                    flee.target = hit.transform;
                    currentState = prey.Flee;
                }

            }
        }
      
       
        if (currentState == prey.Wander)
        {
            currentHunger -= .5f;
            agent.speed = 3;
        }

      
      
        if (currentHunger >= maxHunger)
        {
            currentState = prey.Wander;
        }
        if (health <= 0)
        {
            currentState = prey.die;
        }

    }
    public void takeDamage(float damage)
    {
        health -= damage;
    }
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        switch (currentState)
        {
            case prey.Wander:
                agent.destination = wander.wanderingPoints();
                break;
            case prey.Eat:
                float distance = Vector3.Distance(transform.position, food.transform.position);
                agent.destination = food.transform.position;
                if (distance < 1)
                {
                    currentHunger += 2;
                    if (health < maxHealth)
                    {
                        health += 1;
                    }
                }
                break;
            case prey.Flee:
                
                    agent.destination = flee.returnTarget();
                    agent.speed = 10;
                if (Vector3.Distance(flee.target.transform.position, transform.position) >= 15)
                {
                    currentState = prey.Wander;
                }
                break;
            case prey.die:
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                break;
        }
        Debug.DrawLine(transform.position, agent.destination, Color.red);
        switchControl();
    }
}


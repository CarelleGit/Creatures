using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum prey
{
    Eat, Wander, Flee
}

public class Prey : MonoBehaviour
{
    Wander wander;
    Flee flee;

    public float currentHunger;
    public float maxHunger;
    public int health;
    public int maxHealth;
    public prey currentState;

    public GameObject food;
    public GameObject hunter;
    public GameObject predetor;
  

    NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<Wander>();
        flee = GetComponent<Flee>();

        hunter = GameObject.FindGameObjectWithTag("Hunter");
        predetor = GameObject.FindGameObjectWithTag("Preditor");
        food = GameObject.FindGameObjectWithTag("Food");

        currentHunger = maxHunger;
        health = maxHealth;
    }
    void switchControl()
    {
        if(currentState == prey.Wander)
        {
            currentHunger -= .5f;
        }

        if (currentHunger <= 0)
        {
            currentState = prey.Eat;
        }

        if(Vector3.Distance(predetor.transform.position, transform.position) <= 10 || Vector3.Distance(hunter.transform.position, transform.position) <= 10)
        {
            currentState = prey.Flee;
        }
        else
        {
            currentState = prey.Wander;
        }
        if (currentHunger >= maxHunger)
        {
            currentState = prey.Wander;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch(currentState)
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
              if(Vector3.Distance(predetor.transform.position, transform.position) <= 10)
                {
                    agent.destination = flee.returnPreditor();
                }
                if (Vector3.Distance(hunter.transform.position, transform.position) <= 10)
                {
                    agent.destination = flee.returnHunter();
                }
                break;
        }
        switchControl();
    }
}

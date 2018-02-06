using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    Vector3 desiredVelocity;
    public float speed;


    GameObject hunter;
    GameObject preditor;

    private void Start()
    {
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        preditor = GameObject.FindGameObjectWithTag("Preditor");
    }

    public Vector3 returnHunter()
    {
        return (hunter.transform.position - transform.position) * -1 + transform.position;
    }
    public Vector3 returnPreditor()
    {
        return (preditor.transform.position - transform.position) * -1 + transform.position;
    }

}


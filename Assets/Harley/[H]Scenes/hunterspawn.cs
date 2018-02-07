using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunterspawn : MonoBehaviour {
    public GameObject Enemey;
    public float range;
    public float time;
    private float timer;
    public int max;
    public int currentanmontofenemys;
    public List<GameObject> listOfEnemies;

    // Use this for initialization
    void Start()
    {
        listOfEnemies = new List<GameObject>();
    }



    public void SpawnFunction()
    {
        //Spawn The enemy just like the pick up
        GameObject spawnedEnemey = Instantiate(Enemey);
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);
        spawnedEnemey.transform.position = transform.position + new Vector3(x, 0, z);
        listOfEnemies.Add(spawnedEnemey);
        currentanmontofenemys += 1;
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && currentanmontofenemys < max)
        {
            SpawnFunction();
            timer = time;
        }
    }
}

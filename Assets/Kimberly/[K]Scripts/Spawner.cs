using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject creature;

    public float spawnInt;
    public float range;
    public float timer;
    public int limit;
    public int creatureCount;

    public List<GameObject> listOfCreatures;

    // Use this for initialization
    void Start()
    {
        timer = spawnInt;
        listOfCreatures = new List<GameObject>();
    }

    void enemies()
    {
        if (creatureCount <= limit)
        {
            GameObject spawnCreature = Instantiate(creature);
            float ranx = Random.Range(-range, range);
            float rany = Random.Range(-range, range);
            spawnCreature.transform.position = transform.position + new Vector3(ranx, 0, rany);
            listOfCreatures.Add(spawnCreature);
            for (int i = 0; i < listOfCreatures.Count; i++)
            {
                if (creature == null)
                {
                    Destroy(listOfCreatures[i]);
                    listOfCreatures.RemoveAt(i);
                    creatureCount--;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (creatureCount <= limit - 1)
            {
                enemies();
                timer = spawnInt;
                creatureCount++;
            }
        }
        
    }
}


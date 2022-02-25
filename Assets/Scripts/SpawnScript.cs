using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public List<GameObject> rabbits;
    public List<GameObject> selectedSpawns;
    public List<GameObject> possibleSpawns;
    public GameObject rabbit;

    // Start is called before the first frame update
    void Start()
    {
        int spawnAdded = 0;

        while(spawnAdded < 7)
        {
            //Random spawn
            GameObject spawn = this.possibleSpawns[Random.Range(0, this.possibleSpawns.Count)];
            if(spawn != null)
            {
                this.selectedSpawns.Add(spawn);
                this.possibleSpawns.Remove(spawn);
                spawnAdded++;
            }
        }

        foreach(GameObject spawn in this.possibleSpawns)
        {
            spawn.SetActive(false);
        }

        foreach (GameObject spawn in this.selectedSpawns)
        {
            GameObject rabbit = Instantiate(this.rabbit.transform, spawn.transform.position, new Quaternion(-1, 0, 0, 1)).gameObject;
            this.rabbits.Add(rabbit);
            spawn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

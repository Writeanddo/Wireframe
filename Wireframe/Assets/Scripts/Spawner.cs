using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform playerPos;
    public float xSpawnOffset;
    public float zSpawnOffset;
    public float distBtwnSpawns;
    public GameObject[] prefabs;
    public float[] ratios;

    float ratioTotal;
    float lastSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        foreach(float r in ratios)
        {
            ratioTotal += r;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerPos.position.z > lastSpawnPos + distBtwnSpawns)
        {
            lastSpawnPos = playerPos.position.z;
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPos = new Vector3(playerPos.position.x + Random.Range(-xSpawnOffset, xSpawnOffset), 0, playerPos.position.z + zSpawnOffset);
        float random = Random.Range(0, ratioTotal);
        float ratioCounter = 0;
        for(int i = 0; i < ratios.Length; i++)
        {
            ratioCounter += ratios[i];
            if(random < ratioCounter)
            {
                GameObject newSpawn = Instantiate(prefabs[i], spawnPos, prefabs[i].transform.rotation);
                newSpawn.transform.eulerAngles = new Vector3(newSpawn.transform.eulerAngles.x, Random.Range(0, 4) * 90f, newSpawn.transform.eulerAngles.z);
                return;
            }
        }
    }
}

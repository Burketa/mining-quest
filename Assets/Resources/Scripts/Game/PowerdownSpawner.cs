using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerdownSpawner : MonoBehaviour
{
    public bool canSpawn = false;
    public Transform powerdownReference, powerdownMin, powerdownMax, instanceReference;
    public float minSpawnTime, maxSpawnTime;
    public GameObject[] powerdowns;

    private int index;
    private List<GameObject> powerdownPool;

    //Otimizacao
    private Vector2 spawnPosition;
    private int minY, maxY;

    void Start()
    {
        powerdownPool = new List<GameObject>();
        minY = (int)powerdownMin.transform.localPosition.y;
        maxY = (int)powerdownMax.transform.localPosition.y;
        spawnPosition = new Vector2(powerdownReference.localPosition.x, 0);

        PopulateItemPool();
    }

    void PopulateItemPool()
    {
        for (int i = 0; i < 100; i++)
        {
            powerdownPool.Add(GenerateRandomItem());
        }
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnItem());
        }
    }

    GameObject GenerateRandomItem()
    {
        index = Random.Range(0, powerdowns.Length);
        GameObject powerdown = Instantiate(powerdowns[index], new Vector2(200, 400), Quaternion.identity);
        return powerdown;
    }

    IEnumerator SpawnItem()
    {
        while (true)
        {
            while (canSpawn)
            {
                spawnPosition.y = Random.Range(powerdownMin.position.y, powerdownMax.position.y);
                if (powerdownPool.Count == 0)
                    PopulateItemPool();
                powerdownPool[0].transform.position = spawnPosition;
                powerdownPool[0].transform.SetParent(instanceReference);
                powerdownPool.RemoveAt(0);
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }
    }
}

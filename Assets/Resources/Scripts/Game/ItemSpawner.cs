using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    public bool canSpawn = false, weightedPct;
    public int poolSize = 100;
	public Transform itemReference, itemMin, itemMax, instanceReference;
    public float minSpawnTime = 1.0f, maxSpawnTime = 2.0f;
    public GameObject[] itens;

    private int index;
    private List<GameObject> itemPool;

    //Otimizacao
    private Vector2 spawnPosition;
    private int minY, maxY;

	void Start()
	{
        itemPool = new List<GameObject>();
        minY = (int)itemMin.transform.localPosition.y;
        maxY = (int)itemMax.transform.localPosition.y;
        spawnPosition = new Vector2(itemReference.localPosition.x, 0);

        PopulateItemPool();
	}

    void PopulateItemPool()
    {
        for(int i = 0; i < poolSize; i++)
            itemPool.Add(GenerateRandomItem());
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnItem());
        }
    }

    GameObject GenerateRandomItem()
    {
        if(!weightedPct)
        {
            index = Random.Range(0, itens.Length);
            GameObject item = Instantiate(itens[index], new Vector2(0, 400), Quaternion.identity);
            return item;
        }
        else
        {
            //Implementar função para gerar itens baseados em chance
            index = Random.Range(0, itens.Length);
            GameObject item = Instantiate(itens[index], new Vector2(0, 400), Quaternion.identity);
            return item;
        }
    }

	IEnumerator SpawnItem()
	{
        while (true)
        {
            while (canSpawn)
            {
                spawnPosition.y = Random.Range(itemMin.position.y, itemMax.position.y);
                if (itemPool.Count == 0)
                    PopulateItemPool();
                itemPool[0].transform.position = spawnPosition;
                itemPool[0].transform.SetParent(instanceReference);
                itemPool.RemoveAt(0);
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }
	}
}

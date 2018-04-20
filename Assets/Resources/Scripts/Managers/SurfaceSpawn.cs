using UnityEngine;
using System.Collections;

public class SurfaceSpawn : MonoBehaviour
{
	public bool canSpawn = true;
	public Transform surfaceReference, smallMin, smallMax, mediumMin, mediumMax, largeMin, largeMax, instanceReference;
	public GameObject[] itens;
	public float minSpawnTime = 1.0f, maxSpawnTime = 2.0f;

	
	private GameObject surfaceInstance;
	private int index;
	
	void Start()
	{
		StartCoroutine(SpawnItem());
	}	
	
	IEnumerator SpawnItem()
	{
		while(true)
		{
			while(canSpawn)
			{
				index = Random.Range(0, itens.Length);
				if(itens[index].tag == "Background_Small")
				{
					surfaceInstance = (GameObject)Instantiate(itens[index], new Vector2(surfaceReference.position.x, Random.Range(smallMin.position.y, smallMax.position.y)), Quaternion.identity);
				}
				else if(itens[index].tag == "Background_Medium")
				{
					surfaceInstance = (GameObject)Instantiate(itens[index], new Vector2(surfaceReference.position.x, Random.Range(mediumMin.position.y, mediumMax.position.y)), Quaternion.identity);	
				}
				else if(itens[index].tag == "Background_Large")
				{
					surfaceInstance = (GameObject)Instantiate(itens[index], new Vector2(surfaceReference.position.x, Random.Range(largeMin.position.y, largeMax.position.y)), Quaternion.identity);
				}
				surfaceInstance.AddComponent<ScenarioMovement>();
				surfaceInstance.transform.parent = instanceReference.transform;
				// Arrumar !
				Destroy(surfaceInstance, 10.0f);
				//
				yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
			}
			yield return null;
		}
	}
}

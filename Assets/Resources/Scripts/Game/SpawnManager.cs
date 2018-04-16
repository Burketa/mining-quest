using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public bool startSpawning = true, canSpawnItem = false, canSpawnPowerUp = false, canSpawnPowerDown = false, weightedPct;
    public int itemPoolSize = 100, powerUpPoolSize = 100, powerDownPoolSize;
    public Transform itemPoolParent, itemReference, itemMin, itemMax, powerUpPoolParent, powerUpReference, powerUpMin, powerUpMax, powerDownPoolParent, powerDownReference, powerDownMin, powerDownMax, instanceReference;
    public float minItemSpawnTime = 1.0f, maxItemSpawnTime = 2.0f, minPowerUpSpawnTime = 1.0f, maxPowerUpSpawnTime = 2.0f, minPowerDownSpawnTime = 1.0f, maxPowerDownSpawnTime = 2.0f;
    public List<Item> possibleItens, possiblePowerUps, possiblePowerDowns;
    public List<GameObject> itemPool, powerUpPool, powerDownPool;

    //Otimizacao
    private Vector2 spawnPosition;
    private int itemMinY, itemMaxY, powerUpMinY, powerUpMaxY, powerDownMinY, powerDownMaxY;
    private int index;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        itemPool = new List<GameObject>();
        powerUpPool = new List<GameObject>();
        powerDownPool = new List<GameObject>();
        itemMinY = (int)itemMin.transform.localPosition.y;
        itemMaxY = (int)itemMax.transform.localPosition.y;
        powerUpMinY = (int)powerUpMin.transform.localPosition.y;
        powerUpMaxY = (int)powerUpMax.transform.localPosition.y;
        powerDownMinY = (int)powerDownMin.transform.localPosition.y;
        powerDownMaxY = (int)powerDownMax.transform.localPosition.y;
        spawnPosition = new Vector2(itemReference.localPosition.x, 0);

        PopulatePool(possibleItens, itemPool, itemPoolSize, itemPoolParent, "item");
        //PopulatePool(possiblePowerUps, powerUpPool, powerUpPoolSize, powerUpPoolParent, "powerup");
        PopulatePool(possiblePowerDowns, powerDownPool, powerDownPoolSize, powerDownPoolParent, "powerdown");
        if(startSpawning)
        { 
            canSpawnItem = true;
            StartCoroutine(SpawnItem());
            //canSpawnPowerUp = true;
            //StartCoroutine(SpawnItem());
            canSpawnPowerDown = true;
            StartCoroutine(SpawnPowerDown());
        }
    }

    void PopulatePool(List<Item> possible, List<GameObject> pool, int size, Transform parent, string type)
    {
        for (int i = 0; i < size; i++)
        {
            pool.Add(GenerateRandomObj(possible, parent, type));
        }
    }
    
    GameObject GenerateRandomObj(List<Item> possible, Transform parent, string type)
    {
        //Gera um indice random entre as possibilidades de itens
        index = Random.Range(0, possible.Count);
        Vector2 defaultPosition = new Vector2(0, -400);
        Quaternion defaultRotation = Quaternion.identity;
        GameObject newObj = new GameObject();
        if (!weightedPct)
        {
            //Cria um novo objeto em branco, nomeia e coloca como child do pool.
            newObj.name = possible[index].itemSprite.name;
            newObj.transform.SetParent(parent);

            //Ajusta o componente Transform
            newObj.transform.position = defaultPosition;
            newObj.transform.rotation = defaultRotation;

            //Ajusta o componente Sprite Renderer
            newObj.AddComponent<SpriteRenderer>();
            _spriteRenderer = newObj.GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = possible[index].itemSprite;
            _spriteRenderer.sortingLayerName = "Default";
            _spriteRenderer.sortingOrder = 0;

            //Ajusta o componente Collider
            newObj.AddComponent<BoxCollider2D>();
            _collider = newObj.GetComponent<BoxCollider2D>();
            _collider.size = new Vector2(32, 32);
            _collider.offset = Vector2.zero;
            _collider.isTrigger = true;

            //Adiciona o ItemScript, PowerUpScript ou PowerDownScript
            switch(type)
            {
                case "item":
                    newObj.AddComponent<ItemScript>();
                    break;
                case "powerup":
                    break;
                    /*
                     *
                     *
                     * ARRUMAR ESSA PARTE ! ESTA ASSIM PORQUE O SPRITE DO POWERDOWN NâO ESTA EM ESCALA !
                     * 
                     * 
                     */
                case "powerdown":
                    newObj.AddComponent<PowerdownScript>();
                    newObj.transform.localScale = new Vector2(0.12f, 0.12f);
                    _collider.size = new Vector2(414, 600);
                    break;
                default:
                    break;
            }
        }
        //Retorna o item recem criado
        return newObj;
    }
    
    IEnumerator SpawnItem()
    {
        while (true)
        {
            while (canSpawnItem)
            {
                spawnPosition.y = Random.Range(itemMin.position.y, itemMax.position.y);
                if (itemPool.Count == 0)
                    PopulatePool(possibleItens, itemPool, itemPoolSize, itemPoolParent, "item");
                itemPool[0].transform.position = spawnPosition;
                itemPool[0].transform.SetParent(instanceReference);
                itemPool.RemoveAt(0);
                yield return new WaitForSeconds(Random.Range(minItemSpawnTime, maxItemSpawnTime));
            }
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            while (canSpawnPowerUp)
            {
                spawnPosition.y = Random.Range(powerUpMin.position.y, powerUpMin.position.y);
                if (powerUpPool.Count == 0)
                    PopulatePool(possiblePowerUps, powerUpPool, powerUpPoolSize, powerUpPoolParent, "powerup");
                powerUpPool[0].transform.position = spawnPosition;
                powerUpPool[0].transform.SetParent(instanceReference);
                powerUpPool.RemoveAt(0);
                yield return new WaitForSeconds(Random.Range(minPowerUpSpawnTime, maxPowerUpSpawnTime));
            }
        }
    }

    IEnumerator SpawnPowerDown()
    {
        while (true)
        {
            while (canSpawnPowerDown)
            {
                spawnPosition.y = Random.Range(powerDownMin.position.y, powerDownMax.position.y);
                if (powerDownPool.Count == 0)
                    PopulatePool(possiblePowerDowns, powerDownPool, powerDownPoolSize, powerDownPoolParent, "powerdown");
                powerDownPool[0].transform.position = spawnPosition;
                powerDownPool[0].transform.SetParent(instanceReference);
                powerDownPool.RemoveAt(0);
                yield return new WaitForSeconds(Random.Range(minPowerDownSpawnTime, maxPowerDownSpawnTime));
            }
        }
    }
}

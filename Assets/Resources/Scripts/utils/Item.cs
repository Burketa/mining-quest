using UnityEngine;

[System.Serializable]
public class Item {
	[SerializeField] public string itemName;                     //Nome do item
	[SerializeField] public int itemID;                          //ID do item
    [SerializeField] public int itemQtd;                         //quantidade do item que o usuario tem
    [SerializeField] public Sprite itemSprite;                   //Sprite do item
    [SerializeField] public string itemCategory;                 //Categoria do Item
    [SerializeField] public string itemBiome;                    //Bioma que o item pertence
    [SerializeField] [TextArea] public string itemDescription;   //itemDesc of the item
    [SerializeField] public int itemValue;                       //itemValue is at start 1
    [SerializeField] [Range(0f, 100f)] public float itemDropPct;                   //itemWeight of the item
    [SerializeField] [Range(1, 10)] public int itemRarity;                      //raridade do item

    public Item(string name, int id, int qtd, Sprite sprite, string category, string biome, string description, int value, float dropPct, int rarity)
    {
		itemName = name;
		itemID = id;
        itemQtd = qtd;
        itemSprite = sprite;
        itemCategory = category;
        itemBiome = biome;
        itemDescription = description;
        itemValue = value;
        itemDropPct = dropPct;
        itemRarity = rarity;
    }
}
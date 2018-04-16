using UnityEngine;

[System.Serializable]
public class Item {
	[SerializeField] public string itemName;    //Nome do item
	[SerializeField] public int itemID;         //ID do item
    [SerializeField] public int itemQtd;       //quantidade de itens que o usuario tem
    [SerializeField] public Sprite itemSprite;  //Sprite do item
    [SerializeField] public string itemCategory;    //Categoria do Item
    [SerializeField] public string itemBiome;       //Bioma que o item pertence
    [SerializeField] public string itemDescription; //itemDesc of the item
    [SerializeField] public int itemValue;           //itemValue is at start 1
    [SerializeField] public float itemDropPct;     //itemWeight of the item
    [SerializeField] public int itemRarity;

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
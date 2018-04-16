using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;                                     //itemName of the item
    public int quantity;                                        //quantity of items user has
    public int itemID;                                         //itemID of the item
    public Sprite itemSprite;
    public string biome;
    public string itemDescription;                             //itemDesc of the item
    public int itemValue = 1;                                   //itemValue is at start 1
    public float dropPctg;                                    //itemWeight of the item
    public int rarity;
}
    
    /*public ItemData()
    {
        itemName = gameObject.name;
        itemID = id;
        itemDescription = desc;
        itemIcon = gameObject.GetComponent<SpriteRenderer>().sprite;
}

    public ItemData(string name, int id, string desc)              //function to create a instance of the Item
    {
        itemName = name;
        itemID = id;
        itemDescription = desc;
        itemIcon = gameObject.GetComponent<SpriteRenderer>().sprite;
    }*/



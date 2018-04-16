using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemDatabase : ScriptableObject {
	[SerializeField]
	public List<Item> database;
	
	void OnEnable() {
		if( database == null )
			database = new List<Item>();
	}
	
	public void Add(Item item) {
		database.Add(item);
	}
	
	public void Remove(Item item) {
		database.Remove(item);
	}
	
	public void RemoveAt(int index) {
		database.RemoveAt(index);
	}
	
	public int COUNT
    {
		get { return database.Count; }
	}
	
	//.ElementAt() requires the System.Linq
	public Item item( int index ) {
		return database.ElementAt( index );
	}

    public Item Search(Item targetItem)
    {
        foreach(Item item in database)
        {
            if (targetItem == item)
                return item;
        }
        return null;
    }

    public List<Item> SearchAllBiome(string biome, bool includeGeral)
    {
        List<Item> returnList = new List<Item>();
        if (includeGeral)
        {
            foreach (Item item in database)
            {
                if (item.itemBiome == biome || item.itemBiome == "geral")
                    returnList.Add(item);
            }
        }
        else
        {
            foreach (Item item in database)
            {
                if (item.itemBiome == biome)
                    returnList.Add(item);
            }
        }
        return returnList;
    }

    public Item SearchByName(string targetItemName)
    {
        foreach (Item item in database)
        {
            if (targetItemName == item.itemName)
                return item;
        }
        return null;
    }

    public List<Item> SearchAllCategory(string targetItemCategory)
    {
        List<Item> returnList = new List<Item>();
        foreach (Item item in database)
        {
            if (item.itemCategory == targetItemCategory)
                returnList.Add(item);
        }
        return returnList;
    }

    public void SortAlphabeticallyAtoZ()
    {
        database.Sort((x, y) => string.Compare(x.itemName, y.itemName));
    }

    public void SortByID()
    {
        database.Sort((x, y) => x.itemID.CompareTo(y.itemID));
    }
}
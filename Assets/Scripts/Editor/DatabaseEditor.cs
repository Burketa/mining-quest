using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class DatabaseEditor : EditorWindow {
	private enum State
	{
		BLANK,
		EDIT,
		ADD
	}
	
	private State state;
	private int selectedItem;
	private string newItemName;
	private int newItemID;
    private int newItemQtd;       
    private Sprite newItemSprite; 
    private string newItemBiome;     
    private string newItemDescription; 
    private int newItemValue; 
    private float newItemDropPct;   
    private int newItemRarity;
    private string newItemCategory;

    private const string DATABASE_PATH = @"Assets/Database/itemDB.asset";
	
	private ItemDatabase itens;
	private Vector2 _scrollPos;
	
	[MenuItem("Database/Item Database %#w")]
	public static void Init() {
		DatabaseEditor window = EditorWindow.GetWindow<DatabaseEditor>();
		window.minSize = new Vector2(800, 400);
		window.Show();
	}
	
	void OnEnable() {
		if (itens == null)
			LoadDatabase();
		
		state = State.BLANK;
	}
	
	void OnGUI() {
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		DisplayListArea();
		DisplayMainArea();
		EditorGUILayout.EndHorizontal();
	}
	
	void LoadDatabase() {
		itens = (ItemDatabase)AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(ItemDatabase));
		
		if (itens == null)
			CreateDatabase();
	}
	
	void CreateDatabase() {
		itens = ScriptableObject.CreateInstance<ItemDatabase>();
		AssetDatabase.CreateAsset(itens, DATABASE_PATH);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	void DisplayListArea() {
		EditorGUILayout.BeginVertical(GUILayout.Width(250));
		EditorGUILayout.Space();
		
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "box", GUILayout.ExpandHeight(true));
		
		for (int cnt = 0; cnt < itens.COUNT; cnt++)
		{
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("-", GUILayout.Width(25)))
			{
				itens.RemoveAt(cnt);
				itens.SortAlphabeticallyAtoZ();
				EditorUtility.SetDirty(itens);
				state = State.BLANK;
				return;
			}
			
			if (GUILayout.Button(itens.item(cnt).itemName, "box", GUILayout.ExpandWidth(true)))
			{
				selectedItem = cnt;
				state = State.EDIT;
			}
			
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.EndScrollView();
		
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		EditorGUILayout.LabelField("Itens: " + itens.COUNT, GUILayout.Width(100));
		
		if (GUILayout.Button("New Item"))
			state = State.ADD;
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
	}
	
	void DisplayMainArea()
	{
		EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
		EditorGUILayout.Space();
		
		switch (state)
		{
		case State.ADD:
			DisplayAddMainArea();
			break;
		case State.EDIT:
			DisplayEditMainArea();
			break;
		default:
			DisplayBlankMainArea();
			break;
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
	}
	
	void DisplayBlankMainArea()
	{
		EditorGUILayout.LabelField(
			"There are 3 things that can be displayed here.\n" +
			"1) Weapon info for editing\n" +
			"2) Black fields for adding a new weapon\n" +
			"3) Blank Area",
			GUILayout.ExpandHeight(true));
	}
	
	void DisplayEditMainArea()
	{
		itens.item(selectedItem).itemName = EditorGUILayout.TextField(new GUIContent("Name: "), itens.item(selectedItem).itemName);
		itens.item(selectedItem).itemID = int.Parse(EditorGUILayout.TextField(new GUIContent("ID: "), itens.item(selectedItem).itemID.ToString()));
        itens.item(selectedItem).itemQtd = int.Parse(EditorGUILayout.TextField(new GUIContent("Qtd: "), itens.item(selectedItem).itemQtd.ToString()));
        //itens.item(selectedWeapon).itemSprite = EditorGUILayout.TextField(new GUIContent("Sprite: "), itens.item(selectedWeapon).itemSprite));
        itens.item(selectedItem).itemBiome = EditorGUILayout.TextField(new GUIContent("Biome: "), itens.item(selectedItem).itemBiome.ToString());
        itens.item(selectedItem).itemDescription = EditorGUILayout.TextField(new GUIContent("Description: "), itens.item(selectedItem).itemDescription.ToString());
        itens.item(selectedItem).itemValue = int.Parse(EditorGUILayout.TextField(new GUIContent("Value: "), itens.item(selectedItem).itemValue.ToString()));
        itens.item(selectedItem).itemDropPct = float.Parse(EditorGUILayout.TextField(new GUIContent("Drop: "), itens.item(selectedItem).itemDropPct.ToString()));
        itens.item(selectedItem).itemRarity = int.Parse(EditorGUILayout.TextField(new GUIContent("Rarity: "), itens.item(selectedItem).itemRarity.ToString()));
        itens.item(selectedItem).itemCategory = EditorGUILayout.TextField(new GUIContent("Category: "), itens.item(selectedItem).itemCategory.ToString());



        EditorGUILayout.Space();
		
		if (GUILayout.Button("Done", GUILayout.Width(100)))
		{
			itens.SortAlphabeticallyAtoZ();
			EditorUtility.SetDirty(itens);
			state = State.BLANK;
		}
	}
	
	void DisplayAddMainArea()
	{
        itens.item(selectedItem).itemName = EditorGUILayout.TextField(new GUIContent("Name: "), itens.item(selectedItem).itemName);
        itens.item(selectedItem).itemID = int.Parse(EditorGUILayout.TextField(new GUIContent("ID: "), itens.item(selectedItem).itemID.ToString()));
        itens.item(selectedItem).itemQtd = int.Parse(EditorGUILayout.TextField(new GUIContent("Qtd: "), itens.item(selectedItem).itemQtd.ToString()));
        //itens.item(selectedWeapon).itemSprite = EditorGUILayout.TextField(new GUIContent("Sprite: "), itens.item(selectedWeapon).itemSprite));
        itens.item(selectedItem).itemBiome = EditorGUILayout.TextField(new GUIContent("Biome: "), itens.item(selectedItem).itemBiome.ToString());
        itens.item(selectedItem).itemDescription = EditorGUILayout.TextField(new GUIContent("Description: "), itens.item(selectedItem).itemDescription.ToString());
        itens.item(selectedItem).itemValue = int.Parse(EditorGUILayout.TextField(new GUIContent("Value: "), itens.item(selectedItem).itemValue.ToString()));
        itens.item(selectedItem).itemDropPct = float.Parse(EditorGUILayout.TextField(new GUIContent("Drop: "), itens.item(selectedItem).itemDropPct.ToString()));
        itens.item(selectedItem).itemRarity = int.Parse(EditorGUILayout.TextField(new GUIContent("Rarity: "), itens.item(selectedItem).itemRarity.ToString()));
        itens.item(selectedItem).itemCategory = EditorGUILayout.TextField(new GUIContent("Category: "), itens.item(selectedItem).itemCategory.ToString());


        EditorGUILayout.Space();
		
		if (GUILayout.Button("Done", GUILayout.Width(100)))
		{
			itens.Add(new Item(newItemName, newItemID, newItemQtd, newItemSprite, newItemCategory, newItemBiome, newItemDescription, newItemValue, newItemDropPct, newItemRarity));
			itens.SortAlphabeticallyAtoZ();
			
			newItemName = string.Empty;
			newItemID = 0;
			EditorUtility.SetDirty(itens);
			state = State.BLANK;
		}
	}
}
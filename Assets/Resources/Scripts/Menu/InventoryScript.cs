using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject buttonTemplate;
    public Transform content;

    private string itensPath = "Sprites/Itens";

    private Sprite[] itens;
    private GameObject currentButton;

	void Start()
    {
        itens = Resources.LoadAll<Sprite>(itensPath);

        foreach(Sprite sprite in itens)
        {
            currentButton = Instantiate(buttonTemplate, content);
            //Arrumar essa linha porcaria...
            currentButton.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        }
	}
}

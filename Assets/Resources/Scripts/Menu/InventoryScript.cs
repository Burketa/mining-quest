using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InventoryScript : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public bool showAll = false;
    public GameObject buttonTemplate;
    public Transform content;
    public Text description;

    [HideInInspector]
    public GameObject selectedButton;
    
    private List<GameObject> buttons = new List<GameObject>();
    private GameObject currentButton;

    //Para o metodo de database
    private int i = 0;

    //Otimizacoes
    private Text buttonText;


    void Start()
    {
        selectedButton = null;

        //Texto padrão sem botão selecionado
        description.text = "Clique no item para ver sua descrição.";
                                                                                        //Cria uma instancia editavel, talvez seja bom pra fazer uma camada de proteção a mais pra adicionar itens novos, fazer uma dessa, adicionar os itens e comparar com os existentes, só dai sobrescrever os valores
                                                                                        //itemDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
        foreach (Item item in itemDatabase.database)
        {
            //Instancia o botao e adiciona a lista para controle
            currentButton = Instantiate(buttonTemplate, content);
            buttons.Add(currentButton);

            //Seta o sprite do botão recem instanciado para o proximo sprite da lista
            currentButton.transform.GetChild(0).GetComponent<Image>().sprite = item.itemSprite;      //Otimizar!

            //Seta a quantidade do botao recem instanciado para o valor da base de dados
            buttonText = currentButton.transform.GetChild(1).GetComponent<Text>();
            buttonText.text = item.itemQtd.ToString();

            //Checa se é pra mostrar todos os itens ou só os maiores que zero
            if (!showAll)
                if (item.itemQtd <= 0)
                    currentButton.SetActive(false);

            //Loga as informações
            Debug.Log("Instanciado: " + item.itemQtd.ToString() + " unidades de " + item.itemName);
        }
    }

    public void ChangeSelectedTo(GameObject obj)
    {
        //Ativa e desativa as bordas
        if (selectedButton != null)
            selectedButton.transform.GetChild(2).GetComponent<Image>().enabled = false;
        selectedButton = obj;
        selectedButton.transform.GetChild(2).GetComponent<Image>().enabled = true;
    }

    public void UpdateDescription(GameObject obj)
    {
        //Atualiza a descrição
        var aux = itemDatabase.SearchByName(obj.transform.GetChild(0).GetComponent<Image>().sprite.name);
        if (aux.itemDescription != "")
            description.text = aux.itemDescription;
        else
            description.text = "Sem descrição ainda =(";
    }

    public void AddItem(string itemSpriteName, int ammount)
    {
        print("Adicionando item: " + itemSpriteName);
        foreach (GameObject currentObj in buttons)
        {
            var spriteHolder = currentObj.transform.GetChild(0);
            var spriteName = spriteHolder.GetComponent<Image>().sprite.name;
            print("Item Holder: " + spriteHolder.name);
            print("Sprite Name: " + spriteHolder.GetComponent<Image>().sprite.name);
            if (itemSpriteName == spriteName)
            {
                var currentAmmount = PlayerPrefs.GetInt("item_" + spriteName);
                PlayerPrefs.SetInt("item_" + spriteName, currentAmmount + ammount);
            }
        }
        PlayerPrefs.Save();
    }

    private void PopularDatabase(Sprite sprite, Sprite[] itemSprites, ItemDatabase itemDatabase)
    {
        
        Sprite newItemSprite = sprite;
        string newItemName = sprite.name;
        int newItemID = i; i++;
        int newItemQtd = 0;
        string newItemBiome = null;
        string newItemDescription = "";
        int newItemValue = 1;
        float newItemDropPct = 100;
        int newItemRarity = 1;
        string newItemCategory = "";

        itemDatabase.Add(new Item(newItemName, newItemID, newItemQtd, newItemSprite, newItemCategory, newItemBiome, newItemDescription, newItemValue, newItemDropPct, newItemRarity));
    }

 }

    /*private void ZerarItens()
    {

        foreach (GameObject currentObj in buttons)
        {
            var spriteHolder = currentObj.transform.GetChild(0);

            print(spriteHolder);
            var spriteName = spriteHolder.GetComponent<Image>().sprite.name;
            print(spriteName);
            PlayerPrefs.SetInt("item_" + spriteName, 0);
            PlayerPrefs.Save();
        }
    }*/

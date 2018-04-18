using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public ItemDatabase database;
    public SpriteRenderer tileEscuro, tileClaro, tileTopo, tileBase, tileCeu, sombra;
    public Transform particles;

    public string sceneString;
    private string spritesPath = "Sprites/Cenarios/";
    private GameManager gameManager;

    void Awake ()
    {
        if (FindObjectOfType<GameManager>())
        {
            gameManager = FindObjectOfType<GameManager>();
            sceneString = CheckChosenLevelString(gameManager.chosenLevel);
        }
        else
            sceneString = CheckChosenLevelString(Random.Range(1, 4));
        //LoadSkin();         //Coloca o sprite no bob de acordo com a skin selecionada
        LoadSprites();      //Coloca os sprites na cena de acordo com a fase selecionada
        LoadItens();        //Coloca os itens na cena de acordo com a fase selecionada
        LoadPowerDowns();   //Coloca os pwoerdowns na cena de acordo com a fase selecionada
        //LoadEtc();          //Coloca os enfeites na cena de acordo com a fase selecionada
        LoadParticles();    //Coloca as particulas do clima na cena de acordo com a fase selecionada
    }

    void LoadSkin()
    {

    }

    string CheckChosenLevelString(int chosenLevel)
    {
        switch (chosenLevel)
        {
            case 1:
                return "Planicie";

            case 2:
                return "Deserto";

            case 3:
                return "Selva";

            case 4:
                return "Oceano";

            default:
                return "";
        }
    }

    void LoadSprites()
    {
        tileEscuro.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_escuro");
        tileClaro.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_claro");
        tileCeu.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_ceu");
        tileTopo.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_topo");
        tileBase.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_base");
        sombra.sprite = Resources.Load<Sprite>(spritesPath + sceneString + "/_sombra");
    }

    void LoadItens()
    {
        GetComponent<SpawnManager>().possibleItens = database.SearchAllBiome(sceneString, true);
    }

    void LoadPowerDowns()
    {
        GetComponent<SpawnManager>().possiblePowerDowns = database.SearchAllCategory("powerdown");
    }

    /*
    void LoadPowerUps()
    {
        GetComponent<SpawnManager>().possibleItens = database.SearchAllBiome(sceneString, true);
    }*/

    void LoadEtc()
    {
        ;
    }

    void LoadParticles()
    {
        switch(sceneString)
        {
            case "Planicie":
                particles.GetChild(0).gameObject.SetActive(true);
                break;

            case "Selva":
                particles.GetChild(1).gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }
}

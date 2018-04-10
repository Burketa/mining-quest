using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;           //Static instance of GameManager which allows it to be accessed by any other script.

    public int chosenLevel = 1; //Cenario escolhido
    public int chosenSkin = 1;  //Skin escolhida

    private int levelCount = 4;  //Numero de cenarios no jogo
    private int skinCount = 2;  //Numero de skins no jogo


    //Variaveis para checar estados do jogo
    public bool gameIsOver = false;

    void Awake()    //Singleton
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeChosenLevelAdd()  //Muda o cenario escolhido para o proximo
    {
        chosenLevel++;
        chosenLevel = Mathf.Clamp(chosenLevel, 1, levelCount);
    }

    public void ChangeChosenLevelSubstract()    //Muda o cenario escolhido para o anterior
    {
        chosenLevel--;
        chosenLevel = Mathf.Clamp(chosenLevel, 1, levelCount);
    }

    public void ChangeChosenSkinAdd()  //Muda o cenario escolhido para o proximo
    {
        chosenSkin++;
        chosenSkin = Mathf.Clamp(chosenSkin, 1, skinCount);
    }

    public void ChangeChosenSkinSubstract()    //Muda o cenario escolhido para o anterior
    {
        chosenSkin--;
        chosenSkin = Mathf.Clamp(chosenSkin, 1, skinCount);
    }






    public void LoadScene(int index)    //Carrega a cena pelo indice
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string stage) //Carrega a cena pelo nome
    {
        SceneManager.LoadScene(stage);
    }
}
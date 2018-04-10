using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateChosenLevel : MonoBehaviour
{
    //Otimizar, colocar referencias no Start e tudo mais ou deixar assim ? qual seria melhor ? Uma linha que busca objetos do tipo game manager a todo frame ou com os metodos start e update ?
    //Super gambiarra, feio pra caramba T.T

	void Update ()
    {
        if(gameObject.name == "chosenLevel")
            GetComponent<Text>().text = FindObjectOfType<GameManager>().chosenLevel.ToString();

        else if (gameObject.name == "chosenSkin")
            GetComponent<Text>().text = FindObjectOfType<GameManager>().chosenSkin.ToString();

        else if (gameObject.name == "skins")
        {
            foreach (Transform obj in transform)
                obj.gameObject.SetActive(false);
            transform.GetChild(FindObjectOfType<GameManager>().chosenSkin - 1).gameObject.SetActive(true);
        }

        else if (gameObject.name == "backgrounds")
        {
            foreach (Transform obj in transform)
                obj.gameObject.SetActive(false);
            transform.GetChild(FindObjectOfType<GameManager>().chosenLevel - 1).gameObject.SetActive(true);
        }
	}
}

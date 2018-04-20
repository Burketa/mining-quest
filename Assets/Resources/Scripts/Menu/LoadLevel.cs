using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadScene(int index)    //Carrega a cena pelo indice
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string stage) //Carrega a cena pelo nome
    {
        SceneManager.LoadScene(stage);
    }
}

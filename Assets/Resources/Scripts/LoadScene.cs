using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void DoLoad(int index)    //Carrega a cena pelo indice
    {
        SceneManager.LoadScene(index);
    }

    public void DoLoad(string stage) //Carrega a cena pelo nome
    {
        SceneManager.LoadScene(stage);
    }
}

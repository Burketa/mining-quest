using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PowerdownScript : MonoBehaviour
{
    StateManager sm;

    private void Start()
    {
        sm = FindObjectOfType<StateManager>();
    }

    private void OnTriggerEnter2D(Collider2D colidedObj)
    {
        if (colidedObj.tag == "Bob")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            switch(gameObject.name)
            {
                case "pd_tnt":
                    Explode(colidedObj.gameObject);
                    break;
                case "pd_skull":
                    Death();
                    break;
            }
        }
        else if (colidedObj.tag == "Collector")
            Destroy(gameObject);
    }

    void Explode(GameObject colidedObj)
    {
        sm.gameOver = true;
        FindObjectOfType<ScenarioMovement>().enabled = false;
        colidedObj.GetComponent<PlayerMovement>().enabled = false;
        colidedObj.GetComponent<HoleManager>().enabled = false;
        colidedObj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("explode");
        Invoke("LoadLevel", 1f);
    }

    void Death()
    {
        sm.gameOver = true;
        FindObjectOfType<ScenarioMovement>().enabled = false;
        GameObject.FindWithTag("Skull").GetComponent<Animation>().Play();
        Invoke("LoadLevel", 2f);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(1);
        sm.gameOver = false;
    }
}

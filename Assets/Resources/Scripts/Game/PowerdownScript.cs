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
            sm.gameOver = true;

            GetComponent<SpriteRenderer>().enabled = false;
            Death();
        }
        else if (colidedObj.tag == "Collector")
            Destroy(gameObject);
    }

    void Death()
    {
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

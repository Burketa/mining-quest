using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private string bestScoreString = "bestScore";
    private Text scoreText;
    private string currentDefault = "Distancia: ", bestDefault = "Best: ";
    private float currentScore = 0;

	void Start ()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
    }

	void Update ()
    {
        AddPoints(Time.deltaTime * 10);
    }

    public void AddPoints(float ammount)
    {
        currentScore += ammount;
        if (currentScore > PlayerPrefs.GetInt(bestScoreString))
        {
            PlayerPrefs.SetInt(bestScoreString, (int)currentScore);
        }
        scoreText.text = currentDefault + ((int)currentScore).ToString("0") + "\n" + bestDefault + PlayerPrefs.GetInt(bestScoreString, 0).ToString();
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}

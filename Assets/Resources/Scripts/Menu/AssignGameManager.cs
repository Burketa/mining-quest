using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignGameManager : MonoBehaviour
{
    public Button play, nextLevel, previousLevel, nextSkin, previousSkin;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        play.onClick.AddListener(delegate { gameManager.LoadScene(1); });

        nextLevel.onClick.AddListener(delegate { gameManager.ChangeChosenLevelAdd(); });
        previousLevel.onClick.AddListener(delegate { gameManager.ChangeChosenLevelSubstract(); });

        nextSkin.onClick.AddListener(delegate { gameManager.ChangeChosenSkinAdd(); });
        previousSkin.onClick.AddListener(delegate { gameManager.ChangeChosenSkinSubstract(); });


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool gameOver = false, gamePaused = false, isRewinding = false;

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void StartRewinding()
    {
        isRewinding = true;
        //FindObjectOfType<ItemSpawner>().enabled = false;
        //FindObjectOfType<PowerdownSpawner>().enabled = false;
        //FindObjectOfType<SurfaceSpawn>().enabled = false;
    }
    public void StopRewindig()
    {
        isRewinding = false;
        //FindObjectOfType<ItemSpawner>().enabled = true;
        //FindObjectOfType<PowerdownSpawner>().enabled = true;
        //FindObjectOfType<SurfaceSpawn>().enabled = true;
    }
}

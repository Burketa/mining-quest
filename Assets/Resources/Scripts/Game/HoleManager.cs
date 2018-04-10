using UnityEngine;
using System.Collections;

public class HoleManager : MonoBehaviour
{
    public bool canUpdate = false;
    public GameObject holePrefab;
    public Transform movingBackground;
    public Transform holeReference;
    public int ammount;

    //Otimizações
    private int index;
    private Transform[] holePool;
    private Vector2 holePosition;
    private StateManager state;

    
    void Awake()
    {
        state = FindObjectOfType<StateManager>();

        holePosition = holeReference.position;

        holePool = new Transform[ammount];

        for (int i = 0; i < ammount; i++)
        {
            holePool[i] = Instantiate(holePrefab, new Vector2(-200, 1000), Quaternion.identity, holeReference).GetComponent<Transform>();
            holePool[i].gameObject.AddComponent<TimeBody>();
        }

    }

    void Update()
    {
        if (!state.gamePaused && !state.isRewinding)
        {
            if (index == ammount - 1)
                index = 0;
            holePool[index].SetParent(movingBackground);
            holePool[index].position = holeReference.position;
            index++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	StateManager stateMagager;

	public float recordTime = 3f;

	List<PointInTime> pointsInTime;

	Rigidbody2D rb;
    ScenarioMovement sm;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
        stateMagager = FindObjectOfType<StateManager>();
        rb = GetComponent<Rigidbody2D>();
        sm = FindObjectOfType<ScenarioMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Return) && !stateMagager.gameOver)
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Return) && !stateMagager.gameOver)
			StopRewind();

        if (stateMagager.isRewinding)
            Rewind();
        else
            Record();
    }

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
			pointsInTime.RemoveAt(0);
		}
        else
			StopRewind();
	}

	void Record ()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
			pointsInTime.RemoveAt(pointsInTime.Count - 1);

        pointsInTime.Insert(0, new PointInTime(transform.position));
	}

	public void StartRewind ()
	{
        stateMagager.StartRewinding();
        sm.enabled = false;
        if (rb)
            rb.isKinematic = true;
    }

	public void StopRewind ()
	{
        stateMagager.StopRewindig();
        sm.enabled = true;
        if (rb)
            rb.isKinematic = false;
    }
}

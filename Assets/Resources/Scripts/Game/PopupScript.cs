using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    [HideInInspector]
    public bool doneEnabling = false, doneDisabling = true;

    public float openSpeed = 20.0f, closeSpeed = 10.0f, timeToClose = 5.0f;
    public int maxBounces = 6;

    private int startingMultiplier = 1, currentMultiplier = 1, bouncesCount;
	private Vector2 startingPosition, moveDirection;
	private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Animator textAnim;
    private Text text;

    void Start ()
	{
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = _transform.localPosition;
        moveDirection = new Vector2(openSpeed * 10.0f, 0);
        textAnim = GetComponentInChildren<Animator>();
        text = GetComponentInChildren<Text>();
    }
	
	public IEnumerator Open()
	{
		doneEnabling = false;
		do
		{
			_rigidbody.AddForce(moveDirection);
			yield return null;
		}
		while(!doneEnabling);
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.inertia = 0;
		yield return new WaitForSeconds(timeToClose);
		StartCoroutine(Close());
	}
	
	public IEnumerator Close()
	{
		doneDisabling = false;
		do
		{
            _transform.localPosition = Vector2.MoveTowards(_transform.localPosition, startingPosition, Time.deltaTime * closeSpeed * 10);
			if (_transform.localPosition.x <= startingPosition.x)
			{
                doneDisabling = true;
                text.text = "x" + startingMultiplier.ToString();
				currentMultiplier = startingMultiplier;
				 if (_transform.GetChild(0).childCount > 0)
                    Destroy(_transform.GetChild(0).GetChild(0).gameObject);
            }
			yield return null;
		}
		while(!doneDisabling);
		yield return null;
	}

	public void AddMultiplier(int amount)
	{
		currentMultiplier += amount;
		text.text = "x" + currentMultiplier.ToString();
        textAnim.SetTrigger("pulse");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bouncer")
        {
            bouncesCount++;
            if (bouncesCount >= maxBounces)
            {
                doneEnabling = true;
                bouncesCount = 0;
            }
        }
    }
}

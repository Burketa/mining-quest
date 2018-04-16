using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    public ItemData itemData;
	private static int activePopup = 0;

    private bool alreadyMoved = false;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Transform popups;
    private Transform[] popup, references;
    private BoxCollider2D _collider2D;

    private void Start()
    {
        if(FindObjectOfType<ItemSpawner>().canSpawn)    //Gambiarra, ajeitar um state manager decende depois
            AddGameComponents();
    }

    void AddGameComponents()
	{
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _collider2D = GetComponent<BoxCollider2D>();
        if(_collider2D == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            _collider2D.size = new Vector2Int(32, 32);
            _collider2D.offset = new Vector2Int(0, 0);
        }
        popups = GameObject.FindWithTag("Popups").transform;
        popup = new Transform[popups.childCount];
        for (int i = 0; i < popups.childCount; i++)
            popup[i] = popups.GetChild(i);
        references = new Transform[popups.childCount];
        for (int i = 0; i < popups.childCount; i++)
            references[i] = popup[i].GetChild(0);
    }
	
	
	private void OnTriggerEnter2D(Collider2D colidedObj)
	{
		if(colidedObj.tag == "Bob")
		{
            FindObjectOfType<ScoreManager>().AddPoints(10); //Hardcoded pra testar
			CheckIfEqual();
			CloseNext();
		}
		else if(colidedObj.tag == "Collector")
			Destroy(gameObject);
	}

	private void CheckIfEqual()
	{
		alreadyMoved = false;
		for(int childIndex = 0; childIndex < popups.childCount; childIndex++)
		{
			if(references[childIndex].childCount > 0)
			{
				if(references[childIndex].GetChild(0).name.Contains(this.name))
				{
					popup[childIndex].GetComponent<PopupScript>().AddMultiplier(1);
					alreadyMoved = true;
					Destroy(gameObject);
					break;
				}
			}
		}

		for(int childIndex = 0; childIndex < popups.childCount; childIndex++)
		{
			if(!alreadyMoved)
			{
				if(references[childIndex].childCount <= 0)
				{
					StartCoroutine(popup[activePopup].GetComponent<PopupScript>().Open());
					MoveItem(activePopup);
					activePopup++;
					if(activePopup >= popups.childCount)
						activePopup = 0;
					break;
				}
			}
		}
	}

	private void MoveItem(int i)
	{
        _collider2D.enabled = false;
		_spriteRenderer.sortingLayerName = "UI";
		_spriteRenderer.sortingOrder = 20;
        _transform.parent = popup[i].GetChild(0);
        _transform.position = _transform.parent.position;
        _transform.localScale = new Vector2(1f, 1f);
	}

	private void CloseNext()
	{
			StartCoroutine(popups.transform.GetChild(activePopup).GetComponent<PopupScript>().Close());
	}
}
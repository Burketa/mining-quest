using UnityEngine;
using System.Collections;

public class PickSound : MonoBehaviour
{
	public bool canPlay = true;
	public float timeToSound = 0.2f;

	private AudioSource thisAudio;


	private void Start()
	{
		thisAudio = GetComponent<AudioSource>();
		StartCoroutine(PlaySound());
	}

	private IEnumerator PlaySound()
	{
		while(true)
		{
			while(canPlay)
			{
				thisAudio.Play();
				yield return new WaitForSeconds(0.15f);
			}
			yield return null;
		}
	}
}

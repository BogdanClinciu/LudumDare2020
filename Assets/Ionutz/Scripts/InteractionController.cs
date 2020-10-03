using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
	[SerializeField] private CanvasGroup InteractionCanvas;
	[SerializeField] private Text InteractionText;

	private Interactable interact = null;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!GetComponent<PlayerController>().ControlEnabled)
			return;

		if (interact == null && other.GetComponent<Interactable>() )
		{
			interact = other.GetComponent<Interactable>();
			InteractionText.text = interact.ActionName;
			StartCoroutine(ShowInteractionCanvas(true));
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!GetComponent<PlayerController>().ControlEnabled)
			return;

		if (interact != null)
		{
			interact = null;
			StartCoroutine(ShowInteractionCanvas(false));
		}
	}

	private IEnumerator ShowInteractionCanvas(bool value)
	{
		float start = value ? 0 : 1;
		float end = value ? 1 : 0;
		float timer = 0.1f;
		for (float counter = 0; counter < timer; counter += Time.deltaTime)
		{
			InteractionCanvas.alpha = Mathf.Lerp(start, end, counter / timer);
			yield return null;
		}
		InteractionCanvas.alpha = end;
	}


	private void Update()
	{
		if (!GetComponent<PlayerController>().ControlEnabled)
			return;

		if(Input.GetButtonDown("Submit"))
		{
			if (interact != null && interact.IsInteractable)
			{
				interact.Interact();
			}
		}
	}
}

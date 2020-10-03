using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractive : Interactable
{
	public override string ActionName => "Open Something";

	public override bool IsInteractable { get; set; } = true;

	public override void Interact()
	{
		IsInteractable = false;
		Debug.Log("OpenDrawer");
		GetComponent<Collider2D>().enabled = false;
	}
}

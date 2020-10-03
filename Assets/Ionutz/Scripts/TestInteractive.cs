using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractive : MonoBehaviour, IInteractable
{
	public string ActionName => throw new System.NotImplementedException();

	public bool Interactable => throw new System.NotImplementedException();

	public void Interact()
	{
		throw new System.NotImplementedException();
	}

}

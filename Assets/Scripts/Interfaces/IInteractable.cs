using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract string ActionName {get;}
	public abstract bool IsInteractable { get; set; }
    public abstract void Interact();
}

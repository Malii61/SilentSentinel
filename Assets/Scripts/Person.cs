using UnityEngine;
public class Person : MonoBehaviour,I_Interactable
{
    protected bool isInteractable = false;
    public virtual void OnFaced()
    {
    }

    public virtual void Interact()
    {
    }

    public virtual void OnInteractEnded()
    {
    }
}
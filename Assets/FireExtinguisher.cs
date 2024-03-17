using UnityEngine;

public class FireExtinguisher : MonoBehaviour,I_Interactable
{
    public void OnFaced()
    {
    }

    public void Interact()
    {
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
    }
}

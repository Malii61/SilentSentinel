using UnityEngine;

public class FireExtinguisher : MonoBehaviour,I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Yangın söndürücü\n[E]");
    }

    public void Interact()
    {
        PlayerController.Instance.hasPlayerFireExtinguisher = true;
        InteractionTextUI.Instance.Hide();
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}

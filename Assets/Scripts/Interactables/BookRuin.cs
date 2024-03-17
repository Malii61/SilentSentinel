using UnityEngine;

public class BookRuin : MonoBehaviour,I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Kitap yığınını kaldır\n [E]");
    }

    public void Interact()
    {
        InteractionTextUI.Instance.Hide();
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
    }
}

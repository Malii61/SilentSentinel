using UnityEngine;

public class MainDoor : MonoBehaviour, I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Okula Gir\n [E]");
    }

    public void Interact()
    {
        GameManager.Instance.ChangeState(GameManager.State.GameStarted);
        InteractionTextUI.Instance.Hide();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}
using UnityEngine;

public class CorridorRuin : MonoBehaviour, I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Kaldır\n [E]");
    }

    public void Interact()
    {
        GameManager.Instance.ChangeState(GameManager.State.FirstRuinCleaned);
        InteractionTextUI.Instance.Hide();
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
    }
}
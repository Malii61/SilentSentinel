using UnityEngine;

public class FoodPlaceDoor : MonoBehaviour, I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Yemekhane\n[E]");
    }

    public void Interact()
    {
        InteractionTextUI.Instance.Hide();
        GetComponent<BoxCollider>().enabled = false;
        GameManager.Instance.ChangeState(GameManager.State.EnteredFoodPlace);
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}
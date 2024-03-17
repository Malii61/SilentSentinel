using UnityEngine;

public class ElectricPanel : MonoBehaviour,I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("Elektriği kapat\n [E]");
    }

    public void Interact()
    {
        GameManager.Instance.ChangeState(GameManager.State.ElectricShutDown);
        InteractionTextUI.Instance.Hide();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}

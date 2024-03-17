using UnityEngine;

public class FirstAidKit : MonoBehaviour,I_Interactable
{
    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("İlk yardım çantası\n [E]");
    }

    public void Interact()
    {
        GameManager.Instance.ChangeState(GameManager.State.FirsAidKitTaken);
        InteractionTextUI.Instance.Hide();
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}

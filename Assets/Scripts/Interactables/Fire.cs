using UnityEngine;

public class Fire : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isLastFire = false;
    public void OnFaced()
    {
        if (!PlayerController.Instance.hasPlayerFireExtinguisher) return;
        InteractionTextUI.Instance.ShowText("Söndür\n[E]");
    }

    public void Interact()
    {
        if (!PlayerController.Instance.hasPlayerFireExtinguisher) return;
        InteractionTextUI.Instance.Hide();
        if (isLastFire)
        {
            GameManager.Instance.ChangeState(GameManager.State.FireStopped);
        }
        Destroy(gameObject);
    }

    public void OnInteractEnded()
    {
        if (!PlayerController.Instance.hasPlayerFireExtinguisher) return;
        InteractionTextUI.Instance.Hide();
    }
}
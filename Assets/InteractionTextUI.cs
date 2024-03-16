using TMPro;
using UnityEngine;

public class InteractionTextUI : MonoBehaviour
{
    public static InteractionTextUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI interactionTMP;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void ShowText(string text)
    {
        interactionTMP.enabled = true;
        interactionTMP.text = text;
    }

    public void Hide()
    {
        interactionTMP.enabled = false;
    }
}
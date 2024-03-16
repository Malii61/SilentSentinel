using System;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    [SerializeField] private Animator _animator;
    private bool isOpen;

    public void OnFaced()
    {
        InteractionTextUI.Instance.ShowText("[E]");
    }

    public void Interact()
    {
        Debug.Log("açtım");
        isOpen = !isOpen;
        _animator.SetBool("isOpen", isOpen);
    }

    public void OnInteractEnded()
    {
        InteractionTextUI.Instance.Hide();
    }
}
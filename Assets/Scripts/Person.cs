using System;
using UnityEngine;

public class Person : MonoBehaviour, I_Interactable
{
    protected Transform player;
    protected bool isInteractable = false;
    protected bool isStanding;
    
    protected virtual void Start()
    {
        GameManager.Instance.OnStateChanged += OnStageChanged;
        player = PlayerController.Instance.transform;
        isStanding = true;
    }

    protected virtual void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
    }

    protected virtual void LateUpdate()
    {
        if (isStanding)
        {
            transform.LookAt(PlayerController.Instance.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    public virtual void OnFaced()
    {
    }

    public virtual void Interact()
    {
    }

    public virtual void OnInteractEnded()
    {
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Person : MonoBehaviour, I_Interactable
{
    [SerializeField] private Transform chatBubble;
    [SerializeField] private TextMeshProUGUI bubbleTMP;
    [SerializeField] private float moveSpeed = 1.5f;
    protected Transform player;
    public Vector3 foodPlacePos;
    protected bool isInteractable = false;
    protected bool isStanding;
    protected bool followPlayer;
    protected Rigidbody rb;
    protected Animator _animator;
    protected readonly string SAD_IDLE = "SadIdle";
    protected readonly string HAPPY_IDLE = "HappyIdle";
    protected readonly string WALK = "Walk";
    protected string currentIdle;

    protected virtual void Start()
    {
        GameManager.Instance.OnStateChanged += OnStageChanged;
        rb = GetComponent<Rigidbody>();
        player = PlayerController.Instance.transform;
        _animator = GetComponentInChildren<Animator>();
        isStanding = true;
        currentIdle = SAD_IDLE;
        chatBubble.gameObject.SetActive(false);
        
    }
    public void ShowBubbleText(string bubbleText)
    {
        chatBubble.gameObject.SetActive(true);
        bubbleTMP.text = "";
        StartCoroutine(WriteText(bubbleText));
    }
    private IEnumerator WriteText(string text)
    {
        string txt = "";
        int textIndex = 0;
        while (txt.Length < text.Length)
        {
            yield return new WaitForSeconds(.03f);
            txt += text[textIndex];
            textIndex++;
            bubbleTMP.text = txt;
        }
    }
  
    protected void FollowPlayer()
    {
        followPlayer = true;
    }
    public void HideBubbleText()
    {
        chatBubble.gameObject.SetActive(false);
    }

    protected virtual void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
    }

    protected virtual void FixedUpdate()
    {
        if (followPlayer && Vector3.Distance(player.position, transform.position) > 3f)
        {
            rb.MovePosition(transform.position +
                            (player.position - transform.position).normalized * (moveSpeed * Time.fixedDeltaTime));
            _animator.Play(WALK);
        }
        else
        {
            _animator.Play(currentIdle);
        }
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
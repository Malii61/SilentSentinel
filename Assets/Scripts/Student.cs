using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Student : Person
{
    [SerializeField] private Transform chatBubble;
    [SerializeField] private TextMeshProUGUI bubbleTMP;
    protected Rigidbody rb;
    protected Collider col;
    protected bool followPlayer;
    [SerializeField] private float moveSpeed = 1.5f;
    public Vector3 foodPlacePos;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        chatBubble.gameObject.SetActive(false);
    }

    public void ShowBubbleText(string bubbleText)
    {
        chatBubble.gameObject.SetActive(true);
        bubbleTMP.text = "";
        StartCoroutine(WriteText(bubbleText));
    }

    protected virtual void FixedUpdate()
    {
        if (followPlayer && Vector3.Distance(player.position, transform.position) > 3f)
        {
            rb.MovePosition(transform.position +
                            (player.position - transform.position).normalized * (moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void HideBubbleText()
    {
        chatBubble.gameObject.SetActive(false);
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
}
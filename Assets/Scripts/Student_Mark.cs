using UnityEngine;

public class Student_Mark : Student
{
    private bool isFirstFaced = true;
    private bool isSecondFaced = false;
    public Vector3 gameStartedPos;
    public Vector3 gameStartedRot;
    public Vector3 ruinCleanedPos;
    public Vector3 ruinCleanedRot;
    private bool isFirstKidTaken;

    protected override void Start()
    {
        base.Start();
        currentIdle = HAPPY_IDLE;
    }

    public override void OnFaced()
    {
        if (isFirstFaced)
        {
            ShowBubbleText("Hahaha! Yine ezik gibi giyinmişsin.");
            isFirstFaced = false;
        }
        else if (isSecondFaced)
        {
            ShowBubbleText("Ah! Lütfen yardım et.");
            isSecondFaced = false;
        }
        else if (isFirstKidTaken)
        {
            InteractionTextUI.Instance.ShowText("Yarayı iyileştir\n[E]");
            ShowBubbleText("Bulmuşsun teşekkürler!");
            Invoke(nameof(HideBubbleText), 3f);
        }
    }

    protected override void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        base.OnStageChanged(sender, e);
        if (e.CurrentState == GameManager.State.GameStarted)
        {
            HideBubbleText();
            isSecondFaced = true;
            rb.constraints = RigidbodyConstraints.None;
            isStanding = false;
            rb.MovePosition(gameStartedPos);
            rb.MoveRotation(Quaternion.Euler(gameStartedRot));
        }
        else if (e.CurrentState == GameManager.State.FirstRuinCleaned)
        {
            currentIdle = SAD_IDLE;
            rb.MovePosition(ruinCleanedPos);
            rb.MoveRotation(Quaternion.Euler(ruinCleanedRot));
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            isStanding = true;
            currentIdle = HAPPY_IDLE;
            ShowBubbleText("Teşekkürler. Yardım kiti bulabilir misin?");
        }
        else if (e.CurrentState == GameManager.State.FirsAidKitTaken)
        {
            isFirstKidTaken = true;
        }
        else if (e.CurrentState == GameManager.State.EnteredFoodPlace)
        {
            rb.MovePosition(foodPlacePos);
            Invoke(nameof(FollowPlayer), 1f);
        }
    }

    public override void Interact()
    {
        if (isFirstKidTaken)
        {
            isFirstKidTaken = false;
            InteractionTextUI.Instance.Hide();
            GameManager.Instance.ChangeState(GameManager.State.FirstAidKitGiven);
            ShowBubbleText("Şimdi çok daha iyiyim!");
            Invoke(nameof(HideBubbleText), 3f);
        }
    }

    public override void OnInteractEnded()
    {
      
    }
}
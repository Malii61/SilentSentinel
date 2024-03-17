using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student_Claire : Student
{
    private bool isFirstFaced = true;

    public override void OnFaced()
    {
        if (isFirstFaced)
        {
            ShowBubbleText("Engelleri kaldırdığın için teşekkürler.");
            currentIdle = HAPPY_IDLE;
            Invoke(nameof(HideBubbleText), 3f);
            isFirstFaced = false;
        }
    }

    protected override void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        base.OnStageChanged(sender, e);
        if (e.CurrentState == GameManager.State.EnteredFoodPlace)
        {
            rb.MovePosition(foodPlacePos);
            Invoke(nameof(FollowPlayer), 1f);
        }
    }
}
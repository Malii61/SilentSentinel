using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : Person
{
    protected override void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        base.OnStageChanged(sender, e);

        if (e.CurrentState == GameManager.State.EnteredFoodPlace)
        {
            currentIdle = HAPPY_IDLE;
            followPlayer = true;
        }
    }
}

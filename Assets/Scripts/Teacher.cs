public class Teacher : Person
{
    protected override void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        base.OnStageChanged(sender, e);
        if (e.CurrentState == GameManager.State.EnteredFoodPlace)
        {
            rb.MovePosition(foodPlacePos);
            currentIdle = HAPPY_IDLE;
            Invoke(nameof(FollowPlayer), 1f);
        }
    }
}

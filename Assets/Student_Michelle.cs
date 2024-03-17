public class Student_Michelle : Student
{
    private bool isFirstFaced = true;

    public override void OnFaced()
    {
        if (isFirstFaced)
        {
            ShowBubbleText("Burdan çıkamıyorum. Yardım et!!");
            isFirstFaced = false;
        }
    }

    protected override void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        base.OnStageChanged(sender, e);
        switch (e.CurrentState)
        {
            case GameManager.State.ElectricShutDown:
                ShowBubbleText("Elektriği kapadığın için teşekkürler.");
                Invoke(nameof(HideBubbleText), 3f);
                break;
            case GameManager.State.EnteredFoodPlace:
                rb.MovePosition(foodPlacePos);
                Invoke(nameof(FollowPlayer), 1f);
                break;
        }
    }
}
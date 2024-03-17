using UnityEngine;

public class FinshTrigger : MonoBehaviour
{
    private bool isFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinished)
        {
            GameManager.Instance.ChangeState(GameManager.State.GameFinished);
        }
    }
}
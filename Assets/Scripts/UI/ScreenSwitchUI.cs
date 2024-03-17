using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenSwitchUI : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private TextMeshProUGUI screenSwitchTMP;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        Hide();
    }

    private void OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        switch (e.CurrentState)
        {
            case GameManager.State.GameStarted:
                ShowBlackScreen("Okula girdikten 1 saat sonra deprem olur..", 2f);
                break;
            case GameManager.State.FirstRuinCleaned:
                ShowBlackScreen("Engel kaldırıldı.", 1f);
                break;
            case GameManager.State.EnteredFoodPlace:
                ShowBlackScreen("Yemekhaneye ulaştın.", 1f);
                break;
            case GameManager.State.GameFinished:
                ShowBlackScreen("Herkesi çıkışa ulaştırdın!", 3f);
                Invoke(nameof(BackToMenu), 2.5f);
                break;
        }
    }

    private void ShowBlackScreen(string text, float duration)
    {
        screenSwitchTMP.enabled = true;
        screenSwitchTMP.text = text;
        blackScreen.enabled = true;
        Invoke(nameof(Hide), duration);
    }

    private void Hide()
    {
        blackScreen.enabled = false;
        screenSwitchTMP.enabled = false;
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    private void Start()
    {
        playBtn.onClick.AddListener((() => SceneManager.LoadScene(1)));
    }
}
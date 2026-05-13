using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartButtonAction);
        quitButton.onClick.AddListener(QuitButtonAction);
    }

    private void RestartButtonAction()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(0);
    }

    private void QuitButtonAction()
    {
        Application.Quit();
        Debug.Log("Quit application");
    }
}

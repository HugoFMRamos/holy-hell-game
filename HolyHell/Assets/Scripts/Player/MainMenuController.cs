using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Canvas Elements")]
    public GameObject tutorialScreen;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame() {
        SceneManager.LoadScene("TestScene");
    }

    public void ShowTutorial() {
        tutorialScreen.SetActive(true);
    }

    public void ReturnTutorial() {
        tutorialScreen.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

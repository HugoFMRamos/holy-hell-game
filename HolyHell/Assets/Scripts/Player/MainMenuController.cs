using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Canvas Elements")]
    public GameObject tutorialScreen;
    public Image img;
    public AudioSource musicSource;
    public AudioClip introClip;
    private bool onOrOff = false;

    private void Awake() {
        Time.timeScale = 1.0f;
    }
    
    private void Start() {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(FadeImage(true));
        Invoke(nameof(EnableMenu), 1.0f);
    }

    public void PlayGame() {
        PlaySong();
        StartCoroutine(FadeImage(false));
        Invoke(nameof(Play), 7.5f);
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

    private IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            EnableMenu();
            // loop over 1 second
            for (float i = 0; i <= 7; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i/7);
                yield return null;
            }
        }
    }

    private void PlaySong() {
        musicSource.clip = introClip;
        musicSource.loop = false;
        musicSource.Play();
    }

    private void EnableMenu() {
        img.gameObject.SetActive(onOrOff);
        onOrOff = !onOrOff;
    }

    private void Play() {
        SceneManager.LoadScene("HH Level 1");
    }
}

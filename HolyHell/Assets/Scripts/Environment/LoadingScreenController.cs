using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [Header("Canvas Elements")]
    public Image img;
    public GameManager gameManager;
    public TextMeshProUGUI tipText;
    public List<string> tipTexts;
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

        GameObject manager = GameObject.Find("GameManager");
        if(manager != null) {
            gameManager = manager.GetComponent<GameManager>();
        }

        SetTipText();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(FadeImage(true));
        Invoke(nameof(EnableMenu), 1.0f);
    }

    private void Update() {
        if(Input.anyKey && onOrOff) {
            PlayGame();
        }
    }

    private void SetTipText()
    {
        int textChosen = Random.Range(0, tipTexts.Count);
        tipText.text = tipTexts[textChosen];
    }

    private void PlayGame() {
        StartCoroutine(FadeImage(false));
        Invoke(nameof(Play), 1.5f);
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
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

    private void EnableMenu() {
        img.gameObject.SetActive(onOrOff);
        onOrOff = !onOrOff;
    }

    private void Play() {
        if(gameManager == null) {
            SceneManager.LoadScene(1);
        } else {
            int nextScene = gameManager.playerData.PreviousScene + 1;
            SceneManager.LoadScene(nextScene);
        }
    }
}

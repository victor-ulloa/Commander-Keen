using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    [Header("Button")]
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button backButton;
    [SerializeField] Button resumeGame;
    [SerializeField] Button returnToMenu;

    [Header("Menu")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject pauseMenu;

    [Header("Slider")]
    [SerializeField] Slider volSlider;

    [Header("Text")]
    [SerializeField] Text volSliderText;

    // Start is called before the first frame update
    void Start() {
        if (startButton) {
            startButton.onClick.AddListener(() => StartGame());
        }
        if (settingsButton) {
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }
        if (quitButton) {
            quitButton.onClick.AddListener(() => QuitGame());
        }
        if (backButton) {
            backButton.onClick.AddListener(() => ShowMainMenu());
        }
        if (volSlider) {
            volSlider.onValueChanged.AddListener((value) => SliderValueChange(value));
            volSliderText.text = volSlider.value.ToString();
        }

        if (resumeGame) {
            resumeGame.onClick.AddListener(() => ResumeGame());
        }
        if (returnToMenu) {
            returnToMenu.onClick.AddListener(() => LoadMenu());
        }
    }

    // Update is called once per frame
    void Update() {
        if(pauseMenu) {
            if (Input.GetKeyDown(KeyCode.P)) {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
            if (pauseMenu.activeSelf) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    void StartGame() {
        SceneManager.LoadScene("Level");
    }

    void ShowSettingsMenu() {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

    void ShowMainMenu() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }


    void SliderValueChange(float value) {
        if (volSliderText) {
            volSliderText.text = value.ToString();
        }
    }

    void ResumeGame() {
        pauseMenu.SetActive(false);
    }
        
    void LoadMenu() {
        SceneManager.LoadScene("Title");
    }
}

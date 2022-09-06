using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public AudioMixer audioMixer;

    [Header("Button")]
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button backButton;
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button returnToMenuButton;

    [Header("Menu")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject pauseMenu;

    [Header("Slider")]
    [SerializeField] Slider volSlider;

    [Header("Toggle")]
    [SerializeField] Toggle muteToggle;

    [Header("Text")]
    [SerializeField] Text volSliderText;
    [SerializeField] Text scoreText;


    [SerializeField] Image[] heartArray;

    void Start() {

        // Buttons

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
        if (resumeGameButton) {
            resumeGameButton.onClick.AddListener(() => ResumeGame());
        }
        if (returnToMenuButton) {
            returnToMenuButton.onClick.AddListener(() => LoadMenu());
        }

        // Sliders

        if (volSlider) {
            volSlider.onValueChanged.AddListener((value) => SfxSliderValueChange(value));
            volSlider.value = AudioListener.volume * 100;
            volSliderText.text = volSlider.value.ToString();
        }

        // Toggles

        if (muteToggle) {
            muteToggle.onValueChanged.AddListener((value) => MuteStatusChange(value));
            muteToggle.isOn = AudioListener.pause;
        }

        // Texts

        if (heartArray.Length > 0) {
            GameManager.instance.OnLifeValueChaged.AddListener((value) => UpdateLives(value));
        }
        if (scoreText) {
            scoreText.text = GameManager.instance.score.ToString();
            GameManager.instance.OnScoreValueChanged.AddListener((value) => UpdateScore(value));
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


    void MusicSliderValueChange(float value) {
        if (volSliderText) {
            volSliderText.text = value.ToString();
            audioMixer.SetFloat("MusicVol", value - 80);
        }
    }

    void SfxSliderValueChange(float value) {
        if (volSliderText) {
            volSliderText.text = value.ToString();
            audioMixer.SetFloat("SFXVol", value - 80);
        }
    }

    void ResumeGame() {
        pauseMenu.SetActive(false);
    }
        
    void LoadMenu() {
        SceneManager.LoadScene("Title");
    }

    void MuteStatusChange(bool muted) {
        AudioListener.pause = muted;
    }

    void UpdateLives(int value) {
        for (int i = 0; i < heartArray.Length; i++) {
            if (i < value) {
                heartArray[i].enabled = true;
            } else {
                heartArray[i].enabled = false;
            }
        }
    }

    void UpdateScore(int value) {
        scoreText.text = value.ToString();
    }
}

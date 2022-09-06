using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    [Header("Mixer")]
    [SerializeField] AudioMixer audioMixer;

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
    [SerializeField] Slider masterSoundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [Header("Toggle")]
    [SerializeField] Toggle muteToggle;

    [Header("Text")]
    [SerializeField] Text masterSoundSliderText;
    [SerializeField] Text musicSliderText;
    [SerializeField] Text sfxSliderText;
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

        if (masterSoundSlider) {
            masterSoundSlider.onValueChanged.AddListener((value) => MasterSliderValueChange(value));
            float volValue;
            audioMixer.GetFloat("MasterVol", out volValue);
            masterSoundSlider.value = volValue + 80;
            masterSoundSliderText.text = masterSoundSlider.value.ToString();
        }

        if (musicSlider) {
            musicSlider.onValueChanged.AddListener((value) => MusicSliderValueChange(value));
            float volValue;
            audioMixer.GetFloat("MusicVol", out volValue);
            musicSlider.value = volValue + 80;
            musicSliderText.text = musicSlider.value.ToString();
        }

        if (sfxSlider) {
            sfxSlider.onValueChanged.AddListener((value) => SfxSliderValueChange(value));
            float volValue;
            audioMixer.GetFloat("SFXVol", out volValue);
            sfxSlider.value = volValue + 80;
            sfxSliderText.text = sfxSlider.value.ToString();
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

    void MasterSliderValueChange(float value) {
        if (masterSoundSliderText) {
            masterSoundSliderText.text = value.ToString();
            audioMixer.SetFloat("MasterVol", value - 80);
        }
    }

    void MusicSliderValueChange(float value) {
        if (musicSliderText) {
            musicSliderText.text = value.ToString();
            audioMixer.SetFloat("MusicVol", value - 80);
        }
    }

    void SfxSliderValueChange(float value) {
        if (sfxSliderText) {
            sfxSliderText.text = value.ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance = null;
    public static GameManager instance {
        get { return _instance; }
        set { _instance = value; }
    }

    // PLAYER

    [SerializeField] PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance; 
    [HideInInspector] public Transform currentSpawnPoint;
    [HideInInspector] public Level currentLevel;


    // LIVES 

    [SerializeField] int maxLives = 5;

    private int _lives = 3;
    public int lives {
        get { return _lives; }
        set {

            if (_lives > value) {
                Respawn();
            }

            if (value <= 0) {
                GameOver();
            }

            _lives = value;
            if (_lives > maxLives) {
                _lives = maxLives;
            }

            Debug.Log("Lives are set to:" + lives.ToString());
        }
    }

    // SCORE

    private int _score = 0;
    public int score {
        get { return _score; }
        set {
            _score = value;

            Debug.Log("Your score is:" + score.ToString());
        }
    }

    // Start is called before the first frame update
    void Start() {
        if (instance) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneManager.GetActiveScene().name == "Level" || SceneManager.GetActiveScene().name == "GameOver") {
                SceneManager.LoadScene("Title");
            } else {
                SceneManager.LoadScene("Level");
            }
        }
    }

    void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void SpawnPlayer(Transform spawnLocation) {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

    }

    void Respawn() {
        playerInstance.transform.position = currentSpawnPoint.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance = null;
    public static GameManager instance {
        get { return _instance; }
        set { _instance = value;  }
    }

    // PLAYER

    PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance; 
    [HideInInspector] public Transform currentSpawnPoint;
    //[HideInInspector] public Level level;


    // LIVES 

    const int MAX_LIVES = 5;

    private int _lives = 3;
    public int lives {
        get { return _lives; }
        set {

            if (_lives > value) {
                Respawn();
            }

            // if (_lives <= 0)
            // Game over

            _lives = value;
            if (_lives > MAX_LIVES) {
                _lives = MAX_LIVES;
            }

            Debug.Log("Lives are set to:" + lives.ToString());
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
            if (SceneManager.GetActiveScene().name == "Level") {
                SceneManager.LoadScene("Title");
            } else {
                SceneManager.LoadScene("Level");
            }
        }
    }

    void GameOver() {
        // Go to game over screen
    }

    public void SpawnPlayer(Transform spawnLocation) {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

    }

    void Respawn() {
        playerInstance.transform.position = currentSpawnPoint.position;
    }
}

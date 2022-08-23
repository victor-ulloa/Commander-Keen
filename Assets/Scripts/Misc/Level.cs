using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public int startingLives = 3;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start() {
        GameManager.instance.lives = startingLives;
        GameManager.instance.currentLevel = this;
        GameManager.instance.currentSpawnPoint = spawnPoint;
        GameManager.instance.SpawnPlayer(spawnPoint);
    }

    void UpdateCheckpoint(Transform spawnPoint) {
        GameManager.instance.currentSpawnPoint = spawnPoint;
    }
}

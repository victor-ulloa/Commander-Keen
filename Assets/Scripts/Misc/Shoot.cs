using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Shoot : MonoBehaviour {

    PlayerController playerController;

    [SerializeReference] float projectileSpeed;
    [SerializeReference] Transform spawnPoint;
    [SerializeReference] Projectile projectilePrefab;
    // Start is called before the first frame update
    void Start() {
        playerController = GetComponent<PlayerController>();

        if (projectileSpeed <= 0) {
            projectileSpeed = 7.0f;
        }

        if (!spawnPoint && !projectilePrefab) {
            Debug.Log("Please set up the default values in inspector for the Shoot script on: " + gameObject.name);
        }
    }

    public void Fire() {
        Projectile currentProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        currentProjectile.speed = playerController.facingRight ? projectileSpeed : -projectileSpeed;
    }
}
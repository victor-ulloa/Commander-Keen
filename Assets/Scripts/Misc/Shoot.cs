using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Shoot : MonoBehaviour {

    PlayerController playerController;
    Enemy enemy;
    AudioSourceManager sfxManager;

    [SerializeReference] float projectileSpeed;
    [SerializeReference] Transform spawnPoint;
    [SerializeReference] Transform spawnPointBottom;
    [SerializeReference] Projectile projectilePrefab;
    [SerializeReference] Projectile knifePrefab;
    [SerializeReference] AudioClip fireSfx;

    void Start() {
        playerController = GetComponent<PlayerController>();
        enemy = GetComponent<Enemy>();
        sfxManager = GetComponent<AudioSourceManager>();

        if (projectileSpeed <= 0) {
            projectileSpeed = 7.0f;
        }

        if (!spawnPoint && !projectilePrefab) {
            Debug.Log("Please set up the default values in inspector for the Shoot script on: " + gameObject.name);
        }
    }

    public void Fire() {
        Projectile currentProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        if (playerController != null) {
            currentProjectile.speed = playerController.facingRight ? projectileSpeed : -projectileSpeed;
        } else if (enemy != null) {
            currentProjectile.speed = enemy.facingRight ? projectileSpeed : -projectileSpeed;
        }
        sfxManager.Play(fireSfx);
    }

    public void DropKnife() {
        Projectile currentProjectile = Instantiate(knifePrefab, spawnPointBottom.position, spawnPointBottom.rotation);
        if (playerController != null) {
            currentProjectile.speed = projectileSpeed;
        } 
        sfxManager.Play(fireSfx);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy {

    [SerializeField] float turretRange = 10;

    public float projectileFireRate;

    float timeSinceLastFire;

    Vector3 playerRelativeLocation;


    // Start is called before the first frame update
    public override void Start() {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;
        if (turretRange <= 0)
            turretRange = 10.0f;
    }

    // Update is called once per frame
    void Update() {
        AnimatorClipInfo[] currentClips = animator.GetCurrentAnimatorClipInfo(0);
        playerRelativeLocation = GameManager.instance.playerInstance.transform.position - transform.position;

        if (playerRelativeLocation.x > 0 && !facingRight) {
            flip();
        } else if (playerRelativeLocation.x < 0 && facingRight) {
            flip();
        }

        if (currentClips[0].clip.name != "TurretFire" && Mathf.Abs(playerRelativeLocation.x) < turretRange) {
            if (Time.time >= timeSinceLastFire + projectileFireRate) {
                animator.SetTrigger("fire");
                timeSinceLastFire = Time.time;
            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet")) {
            animator.SetTrigger("shot");
        }
    }

    public override void DestroyMyself() {
        base.DestroyMyself();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy {
    public float projectileFireRate;

    float timeSinceLastFire;


    // Start is called before the first frame update
    public override void Start() {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;
    }

    // Update is called once per frame
    void Update() {
        AnimatorClipInfo[] currentClips = animator.GetCurrentAnimatorClipInfo(0);

        if (currentClips[0].clip.name != "TurretFire") {
            if (Time.time >= timeSinceLastFire + projectileFireRate) {
                animator.SetTrigger("fire");
                timeSinceLastFire = Time.time;
            }
        }
    }

    public override void DestroyMyself() {
        base.DestroyMyself();
    }
}
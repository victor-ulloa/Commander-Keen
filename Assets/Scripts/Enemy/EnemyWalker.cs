using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy {
    [SerializeReference] float speed = 2f;


    // Start is called before the first frame update
    public override void Start() {
        base.Start();
        if (speed <= 0) { speed = 0.5f; }
    }

    void Update() {
        AnimatorClipInfo[] currentClips = animator.GetCurrentAnimatorClipInfo(0);

        if (currentClips[0].clip.name == "walk") {
            if (facingRight) {
                rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            } else {
                rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            }
        }
    }


    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);

        Debug.Log("Enemy Walker Took " + damage + " damage");
    }


    public override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Bullet")) {
            DestroyMyself();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Barrier") {
            flip();
        }
    }

    
}

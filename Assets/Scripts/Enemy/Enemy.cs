using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class Enemy : MonoBehaviour {
    Rigidbody2D rigidBody;
    Animator animator;

    [SerializeReference] float speed = 2f;
    [SerializeReference] bool facingRight = true;
    [SerializeReference] float rightIndex = 1f;
    [SerializeReference] float leftIndex = 3f;

    Vector2 maxRight;
    Vector2 maxLeft;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (speed <= 0) { speed = 0.5f; }
        if (rightIndex <= 0) { rightIndex = 10; }
        if (leftIndex <= 0) { leftIndex = 10; }

        maxRight = new Vector2(transform.position.x + rightIndex, transform.position.y);
        maxLeft = new Vector2(transform.position.x - leftIndex, transform.position.y);

    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, facingRight ? maxRight : maxLeft, Time.deltaTime * speed);

        if (transform.position.x == maxRight.x || transform.position.x == maxLeft.x) { flip(); }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(gameObject);
        }

        flip();
    }

    void flip() {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}

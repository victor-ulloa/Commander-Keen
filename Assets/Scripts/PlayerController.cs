using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float speed = 0.5f;
    public int jumpForce = 300;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (speed <= 0) { speed = 0.5f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.02f; }

    }

    // Update is called once per frame
    void Update() {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rigidBody.velocity.y);
        rigidBody.velocity = moveDirection;

        animator.SetFloat("moveValue", Mathf.Abs(hInput));
        animator.SetBool("isGrounded", isGrounded);

        // TODO: add sprite fliping here
    }
}

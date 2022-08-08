using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Shoot))]

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigidBody;
    Animator animator;

    [SerializeReference] float speed = 0.5f;
    [SerializeReference] int jumpForce = 300;
    [SerializeReference] public bool isGrounded;
    [SerializeReference] LayerMask groundLayer;
    [SerializeReference] Transform groundCheck;
    [SerializeReference] float groundCheckRadius = 0.02f;
    [SerializeReference] public bool facingRight = true;

    Coroutine jumpForceChange;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (speed <= 0) { speed = 0.5f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.02f; }

    }

    // Update is called once per frame
    void Update() {
        AnimatorClipInfo[] currentPlayingClip = animator.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (currentPlayingClip.Length > 0) {
            if (Input.GetButtonDown("Fire1") && currentPlayingClip[0].clip.name != "attack") {
                animator.SetTrigger("shoot");
            } else if (currentPlayingClip[0].clip.name == "attack" || currentPlayingClip[0].clip.name == "jumpAttack") {
                rigidBody.velocity = Vector2.zero;
            } else {
                rigidBody.velocity = new Vector2(hInput * speed, rigidBody.velocity.y);
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(Vector2.up * jumpForce);
        }

        animator.SetFloat("moveValue", Mathf.Abs(hInput));
        animator.SetBool("isGrounded", isGrounded);

        // Flip
        if (hInput > 0 && !facingRight) {
            flip();
        } else if ( hInput < 0 && facingRight) {
            flip();
        }
    }

    void flip() {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    public void StartJumpforceChange() {
        if (jumpForceChange != null) {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
        }
        jumpForceChange = StartCoroutine(JumpForceChange());
    }

    IEnumerator JumpForceChange() {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
    }
}

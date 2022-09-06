using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(Shoot))]

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigidBody;
    Animator animator;
    [HideInInspector] public AudioSourceManager sfxManager;

    [SerializeReference] float speed = 0.5f;
    [SerializeReference] int jumpForce = 300;
    [SerializeReference] public bool isGrounded;
    [SerializeReference] LayerMask groundLayer;
    [SerializeReference] Transform groundCheck;
    [SerializeReference] float groundCheckRadius = 0.02f;
    [SerializeReference] public bool facingRight = true;

    [SerializeReference] AudioClip jumpSfx;
    [SerializeReference] AudioClip deadSfx;

    Coroutine jumpForceChange;
    Coroutine speedChange;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sfxManager = GetComponent<AudioSourceManager>();

        if (speed <= 0) { speed = 0.5f; }
        if (jumpForce <= 0) { jumpForce = 300; }
        if (groundCheckRadius <= 0) { groundCheckRadius = 0.02f; }

        GameManager.instance.OnPlayerDeath.AddListener(() => onPlayerDeath());

    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 0) {
            return;
        }

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
            sfxManager.Play(jumpSfx);
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            GameManager.instance.lives--;
        }
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
        jumpForceChange = null;
        jumpForce /= 2;
    }

    public void StartSpeedChange() {
        if (speedChange != null) {
            StopCoroutine(speedChange);
            speedChange = null;
            speed /= 2;
        }
        speedChange = StartCoroutine(SpeedChange());
    }

    IEnumerator SpeedChange() {
        speed *= 2;
        yield return new WaitForSeconds(5.0f);
        speedChange = null;
        speed /= 2;
    }

    void onPlayerDeath() {
        sfxManager.Play(deadSfx);
    }

}

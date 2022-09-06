using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSourceManager))]

public class Enemy : MonoBehaviour {

    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSourceManager sfxManager;

    [SerializeReference] public bool facingRight = true;

    protected int _health;
    [SerializeReference] public int maxHealth;

    public int health {
        get { return _health; }
        set {
            _health = value;

            if (_health > maxHealth) {
                _health = maxHealth;
            }

            if (_health <= 0)
                Death();

        }
    }

    [SerializeReference] AudioClip deadSfx;

    public virtual void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sfxManager = GetComponent<AudioSourceManager>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
    }

    public virtual void Death() {
        animator.SetTrigger("Death");
    }

    public virtual void TakeDamage(int damage) {
        health -= damage;
    }

    public virtual void DestroyMyself() {
        Debug.Log("TEST");
        sfxManager.Play(deadSfx);
        Destroy(gameObject);
    }

    public virtual void flip() {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}

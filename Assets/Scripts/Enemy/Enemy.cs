using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]

public class Enemy : MonoBehaviour {
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

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

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        Destroy(gameObject);
    }
}

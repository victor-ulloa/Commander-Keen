using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerShoot : MonoBehaviour {

    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            animator.Play("attack");
        }
    }
}

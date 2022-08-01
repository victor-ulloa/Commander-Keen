using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeReference] public float speed;
    [SerializeReference] float lifetime;

    // Start is called before the first frame update
    void Start() {
        if (lifetime <= 0) {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }
}

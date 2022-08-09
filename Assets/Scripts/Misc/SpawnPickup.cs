using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickup : MonoBehaviour {

    [SerializeReference] GameObject[] pickupPrefabs;

    // Start is called before the first frame update
    void Start() {
        Instantiate(pickupPrefabs[Random.Range(0, 3)], transform.position, transform.rotation);
    }

}

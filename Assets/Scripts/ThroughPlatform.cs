using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform : MonoBehaviour {
    PlayerController player;
    BoxCollider2D coll;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        coll = GetComponent<BoxCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "TopCollider") {
            coll.isTrigger = true;
        }

        if (collider.name == "BottomCollider") {
            player.isGrounded = true;
            coll.isTrigger = false;
        }
    }
}

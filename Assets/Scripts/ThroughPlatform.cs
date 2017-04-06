using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlatform : MonoBehaviour {
    PlayerController player;
    BoxCollider2D coll;

    void Start() {
//        player = GameObject.Find("Player").GetComponent<PlayerController>();
//        coll = GetComponent<BoxCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("Platforms");
    }

//    void OnTriggerEnter2D(Collider2D collider) {
//        if(collider.CompareTag("Player")) {
//            if(collider.name == "TopCollider") {
////                coll.isTrigger = true;
//                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), true);
//
//            }
////
//            if(collider.name == "BottomCollider" && !player.falling) {
////                coll.isTrigger = false;
//                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platforms"), false);
//            }
//        }
//    }
//
//    void OnTriggerStay2D(Collider2D collider) {
//        if(collider.CompareTag("Player")) {
//            if(player.falling) {
//                coll.isTrigger = true;
//            }
//
//            if(collider.name == "BottomCollider" && !player.falling) {
//                coll.isTrigger = false;
//            }
//        }
//    }

}

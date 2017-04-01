using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    private bool inRange = false;

    protected virtual void use() { }

    void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.E)) {
            use();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player") {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.name == "Player") {
            inRange = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public Toggle[] connected;

    private bool inRange = false;

    void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.E)) {
            foreach (Toggle obj in connected) {
                obj.toggle();
            }
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

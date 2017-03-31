using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    public int stage;

    private bool inContact = false;

    public Level level;

	// Update is called once per frame
	void Update () {
        if (inContact && level.getStage() == stage && Input.GetKeyDown(KeyCode.E)) {
            level.progress();
        }
	}

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player") {
            inContact = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        if (collider.name == "Player") {
            inContact = false;
        }
    }
}

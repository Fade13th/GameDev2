using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    private bool inRange = false;
    protected String Message { get; set; }

    protected virtual void use() { }

    void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.E)) {
            use();
        }
    }

    void Awake()
    {
        Message = "Press \"E\" to use";
    }

    void OnGUI()
    {
        if (inRange)
        {
            String text = GetPrompt();
            if (text != null)
            {
                GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 120), text);
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

    protected virtual String GetPrompt()
    {
        return Message;
    }
}

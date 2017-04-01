using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {
    public Toggle[] connected;

    protected override void use() {
        foreach (Toggle obj in connected) {
            obj.toggle();
        }
    }
}

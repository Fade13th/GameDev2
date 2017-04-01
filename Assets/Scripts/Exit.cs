using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable {
    public Level level;

    public int stage;

    protected override void use() {
        if (level.getStage() == stage) {
            print("Exit");
        }
    }
}

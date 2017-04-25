using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Interactable {

    public int stage;

    public Level level;

    protected override void use() {
        if (level != null)
            if (stage == level.getStage())
                level.progress();
    }


    protected override String GetPrompt() {
        if (level != null)
            if (level.getStage() == stage)
                return "Press \"E\" to Use";
        return null;
    }
}

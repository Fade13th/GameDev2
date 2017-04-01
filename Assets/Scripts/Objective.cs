using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Interactable {

    public int stage;

    public Level level;

    protected override void use() {
        level.progress();
    }
}

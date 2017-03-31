using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    protected int stage;

    void Start() {
        stage = 0;
    }

    public void progress() {
        stage++;
        updateObj();
    }

    public int getStage() {
        return stage;
    }

    protected virtual void updateObj() { }
}

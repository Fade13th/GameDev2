﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    protected LevelManager manager;

    protected int stage;

    public bool crooked = false;

    protected Text objectiveText;

    private bool fading = false;

    void Start() {
        stage = 0;
        objectiveText = GameObject.Find("Objective").GetComponent<Text>();
        updateObj();

        manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update() {
        if (fading) {
            objectiveText.color = new Color(1, objectiveText.color.g + 0.01f, objectiveText.color.b + 0.01f);
        }
    }

    public void progress() {
        stage++;
        updateObj();
    }

    public int getStage() {
        return stage;
    }

    public virtual void updateObj() {
        fading = true;
        objectiveText.color = Color.red;
    }
}

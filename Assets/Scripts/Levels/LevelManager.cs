﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager levelManager;
    public Level[] levels;
    public Cutscene[] cutscenes;

    int currentScene = 0, currentLevel =-1;

    private Level level;
    private Cutscene scene;

    CanvasGroup levelUI, levelCompleteUI;

    private CanvasGroup canvas;
    private float targetAlpha = 0;
    private bool bFade = false;
    private float step = 0.1f;

    private int experience = 0;
    private int reputation = 0;
    private int infamy = 0;
    private int strikes = 0;
    private int camSpots = 0;
    private int guardSpots = 0;
    private int laserSpots = 0;

    private bool crooked;

    private Text completeRank, completeStrike, completeCamera, completeGuard, completeLaser, completeRep, completeInf;

    void Awake()
    {
        levelManager = this;
    }

    // Use this for initialization
    void Start () {
        if (levels.Length == 0)
            Application.Quit();

        canvas = GetComponent<CanvasGroup>();
        levelUI = GameObject.Find("Level_UI").GetComponent<CanvasGroup>();
        levelCompleteUI = GameObject.Find("LevelComplete").GetComponent<CanvasGroup>();
        levelCompleteUI.alpha = 0;
        levelCompleteUI.blocksRaycasts = false;

        completeRank = GameObject.Find("Rank").GetComponent<Text>();
        completeStrike = GameObject.Find("Strikes").GetComponent<Text>();
        completeCamera = GameObject.Find("CameraSpots").GetComponent<Text>();
        completeGuard = GameObject.Find("GuardSpots").GetComponent<Text>();
        completeLaser = GameObject.Find("LaserSpots").GetComponent<Text>();
        completeRep = GameObject.Find("Reputation").GetComponent<Text>();
        completeInf = GameObject.Find("Infamy").GetComponent<Text>();

        if (cutscenes.Length != 0)
            StartCoroutine(nextScene());
        else
            StartCoroutine(nextLevel());
	}

    void Update() {
        if (bFade) {
            bFade = false;
            targetAlpha = 1;
            StartCoroutine(waitToFade(1.5f));
        }

        if (canvas.alpha > targetAlpha) {
            canvas.alpha -= step;
        }
        else if (canvas.alpha < targetAlpha) {
            canvas.alpha += step;
        }
    }

    public void setCrooked(bool crooked) {
        this.crooked = crooked;
    }

    public void addInfamy(int i) {
        infamy += i;
    }

    public void addReputation(int i) {
        reputation += i;
    }

    public void addExperience(int i) {
        experience += i;
    }

    public void next() {
        if (scene != null)
            OnSceneExit();

        if (level != null)
            OnLevelExit();

        levelCompleteUI.alpha = 0;
        levelCompleteUI.blocksRaycasts = false;

        if (currentScene > currentLevel + 1) {
            StartCoroutine(nextLevel());
        }
        else if (currentScene < cutscenes.Length) {
            StartCoroutine(nextScene());
        }
        else if (currentLevel + 1 < levels.Length) {
            StartCoroutine(nextLevel());
        }
    }

    public IEnumerator nextScene() {
        bFade = true;

        yield return new WaitForSeconds(1.5f);
        targetAlpha = 0;

        OnSceneStart();
        currentScene++;
    }

    public IEnumerator nextLevel() {
        bFade = true;

        yield return new WaitForSeconds(1.5f);
        targetAlpha = 0;
        currentLevel++;

        OnLevelStart();
    }

    public IEnumerator resetLevel()
    {
        bFade = true;
        yield return new WaitForSeconds(1.5f);
        targetAlpha = 0;

        OnLevelExit();

        OnLevelStart();
    }

    private IEnumerator waitToFade(float time) {
        yield return new WaitForSeconds(time);
        targetAlpha = 0;
    }

    private void OnSceneStart() {
        levelUI.alpha = 0;

        scene = GameObject.Instantiate(cutscenes[currentScene]);
    }

    private void OnSceneExit() {
        Destroy(scene.gameObject);
        scene = null;
    }

    private void OnLevelStart() {
        level = GameObject.Instantiate(levels[currentLevel]);

        levelUI.alpha = 1;
        level.GetComponent<Level>().crooked = crooked;
        level.GetComponent<Level>().updateObj();
    }

    private void OnLevelExit() {
        Destroy(level.gameObject);
        level = null;
    }

    public static LevelManager GetLevelManager()
    {
        return levelManager;
    }

    public void laserSpot() {
        laserSpots++;
        StartCoroutine(resetLevel());
    }

    public void guardSpot() {
        guardSpots++;
        StartCoroutine(resetLevel());
    }

    public void cameraSpot() {
        camSpots++;
    }

    public void levelComplete() {
        levelUI.blocksRaycasts = false;

        completeRank.text = Config.getConfig().getRank(experience);
        completeCamera.text = camSpots + "";
        completeGuard.text = guardSpots + "";
        completeLaser.text = laserSpots + "";
        completeStrike.text = Config.getConfig().getStrikes(camSpots, guardSpots, laserSpots) + "";
        completeRep.text = Config.getConfig().getRep(reputation);
        completeInf.text = Config.getConfig().getInf(infamy);

        levelCompleteUI.alpha = 1;
        levelCompleteUI.blocksRaycasts = true;
        levelCompleteUI.interactable = true;
        PlayerController.GetPlayer().cutscene = true;
    }
}

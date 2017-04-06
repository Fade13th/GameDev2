using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager levelManager;
    public Level[] levels;
    public Cutscene[] cutscenes;

    int currentScene = 0, currentLevel =-1;

    private Level level;
    private Cutscene scene;

    CanvasGroup levelUI;

    private CanvasGroup canvas;
    private float targetAlpha = 0;
    private bool bFade = false;
    private float step = 0.1f;

    public int experience = 0;
    public int reputation = 0;
    public int infamy = 0;

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

    public IEnumerator nextScene() {
        bFade = true;

        yield return new WaitForSeconds(1.5f);
        targetAlpha = 0;

        if (currentScene > 0)
            OnSceneExit();

        if (level != null)
            OnLevelExit();

        OnSceneStart();
        currentScene++;
    }

    public IEnumerator nextLevel() {
        bFade = true;

        yield return new WaitForSeconds(1.5f);
        targetAlpha = 0;
        currentLevel++;

        if (level != null)
            OnLevelExit();
        
        if (scene != null)
            OnSceneExit();

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
        levelUI.alpha = 1;

        level = GameObject.Instantiate(levels[currentLevel]);
    }

    private void OnLevelExit() {
        Destroy(level.gameObject);
        level = null;
    }

    public static LevelManager GetLevelManager()
    {
        return levelManager;
    }
}

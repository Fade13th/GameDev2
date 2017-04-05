using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {
    protected Conversation conversation;

    protected delegate void Func();
    protected List<Func> func;

    private bool updating = false;

    protected string resp1, resp2, resp3;

    protected int stage = -1;

    protected PlayerController player;
    protected LevelManager manager;

    public Level nextLevel;

    public Sprite[] sprites;

    protected void updateResp1() {
        conversation.setResponse1(resp1);
    }

    protected void updateResp2() {
        conversation.setResponse2(resp2);
    }

    protected void updateResp3() {
        conversation.setResponse3(resp3);
    }

    // Use this for initialization
    void Start () {
        conversation = GameObject.Find("Conversation").GetComponent<Conversation>();
        func = new List<Func>();

        stepCutscene(0);
        updating = true;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update () {
		if (updating) {
            conversation.disableResponse();

            foreach (Func f in func) {
                f();
            }
            func.Clear();

            updating = false;
        }
	}

    public virtual void stepCutscene(int response) {
        stage++;
        updating = true;
    }

    protected void walk(int direction, float time) {
        player.cutsceneWalk = direction;
        StartCoroutine(WalkWait(time));

    }

    protected IEnumerator WalkWait(float time) {
        yield return new WaitForSeconds(time);
        player.cutsceneWalk = 0;
        
        stepCutscene(0);
    }
}

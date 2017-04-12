using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cutscene : MonoBehaviour {
    protected Conversation conversation;

    protected delegate void Func();
    protected List<Func> func;

    private bool updating = false;

    protected string resp1, resp2, resp3;

    protected int stage = -1;

    protected PlayerController player;
    protected LevelManager manager;

    public Sprite[] sprites;
    public Sprite[] responseIcons;

    protected bool crooked;

    protected void updateResp1() {
        conversation.setResponse1(resp1);
    }

    protected void setResp1Icon(int icon) {
        if (icon == -1) {
            conversation.setResponse1Icon(responseIcons[1]);
        }
        else if (icon == 1) {
            conversation.setResponse1Icon(responseIcons[2]);
        }
        else {
            conversation.setResponse1Icon(responseIcons[3]);
        }
    }

    protected void updateResp2() {
        conversation.setResponse2(resp2);
    }

    protected void setResp2Icon(int icon) {
        if (icon == -1) {
            conversation.setResponse2Icon(responseIcons[1]);
        }
        else if (icon == 1) {
            conversation.setResponse2Icon(responseIcons[2]);
        }
        else {
            conversation.setResponse2Icon(responseIcons[3]);
        }
    }

    protected void updateResp3() {
        conversation.setResponse3(resp3);
    }

    protected void setResp3Icon(int icon) {
        if (icon == -1) {
            conversation.setResponse3Icon(responseIcons[1]);
        }
        else if (icon == 1) {
            conversation.setResponse3Icon(responseIcons[2]);
        }
        else {
            conversation.setResponse3Icon(responseIcons[3]);
        }
    }

    // Use this for initialization
    void Start () {
        conversation = GameObject.Find("Conversation").GetComponent<Conversation>();
        func = new List<Func>();

        player = PlayerController.GetPlayer();
        manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        stepCutscene(0);
        updating = true;
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
        setResp1Icon(0);
        setResp2Icon(0);
        setResp3Icon(0);

        stage++;
        updating = true;
    }

    protected void walk(int direction, float time) {
        player.setCutsceneDirection(direction);
        StartCoroutine(WalkWait(time));
    }

    protected IEnumerator WalkWait(float time) {
        yield return new WaitForSeconds(time);

        player.setCutsceneDirection(0);
                
        stepCutscene(0);
    }
}

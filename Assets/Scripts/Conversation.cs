using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {
    private CanvasGroup playerIcon, otherIcon, all;
    private Text playerDialogue, otherDialogue, otherName;
    private UnityEngine.UI.Button response1, response2, response3;
    private Image playerImage, otherImage;

	// Use this for initialization
	void Awake () {
        playerIcon = GameObject.Find("PlayerIcon").GetComponent<CanvasGroup>();
        otherIcon = GameObject.Find("OtherIcon").GetComponent<CanvasGroup>();
        all = GetComponent<CanvasGroup>();

        playerDialogue = GameObject.Find("PlayerDialogue").GetComponent<Text>();
        otherDialogue = GameObject.Find("OtherDialogue").GetComponent<Text>();
        otherName = GameObject.Find("OtherName").GetComponent<Text>();

        response1 = GameObject.Find("Response1").GetComponent<UnityEngine.UI.Button>();
        response2 = GameObject.Find("Response2").GetComponent<UnityEngine.UI.Button>();
        response3 = GameObject.Find("Response3").GetComponent<UnityEngine.UI.Button>();

        playerImage = GameObject.Find("PlayerImage").GetComponent<Image>();
        otherImage = GameObject.Find("OtherImage").GetComponent<Image>();

        disableResponse();
    }

    public void hide() {
        all.alpha = 0;
        all.blocksRaycasts = false;

        playerDialogue.text = "";
        otherName.text = "";
        otherDialogue.text = "";
    }

    public void show() {
        all.alpha = 1;
        all.blocksRaycasts = true;
    }

    public void hidePlayer() {
        playerIcon.alpha = 0;
        playerIcon.blocksRaycasts = false;
    }

    public void showPlayer() {
        playerIcon.alpha = 1;
        playerIcon.blocksRaycasts = true;
    }

    public void hideOther() {
        otherIcon.alpha = 0;
        otherIcon.blocksRaycasts = false;
    }

    public void showOther() {
        otherIcon.alpha = 1;
        otherIcon.blocksRaycasts = true;
    }

    public void setOtherName(string name) {
        otherName.text = name;
    }

    public void setPlayerImage(Sprite sprite) {
        playerImage.sprite = sprite;
    }

    public void setOtherImage(Sprite sprite) {
        otherImage.sprite = sprite;
    }

    public void setDialogue(string text, bool isPlayer) {
        if (isPlayer) {
            otherDialogue.text = "";
            playerDialogue.text = text;
        }
        else {
            otherDialogue.text = text;
        }
    }

    public void disableResponse() {
        response1.GetComponent<CanvasGroup>().alpha = 0;
        response1.GetComponent<CanvasGroup>().blocksRaycasts = false;

        response2.GetComponent<CanvasGroup>().alpha = 0;
        response2.GetComponent<CanvasGroup>().blocksRaycasts = false;

        response3.GetComponent<CanvasGroup>().alpha = 0;
        response3.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void setResponse1(string text) {
        response1.GetComponent<CanvasGroup>().alpha = 1;
        response1.GetComponent<CanvasGroup>().blocksRaycasts = true;

        response1.GetComponentInChildren<Text>().text = text;
    }

    public void setResponse2(string text) {
        response2.GetComponent<CanvasGroup>().alpha = 1;
        response2.GetComponent<CanvasGroup>().blocksRaycasts = true;

        response2.GetComponentInChildren<Text>().text = text;
    }

    public void setResponse3(string text) {
        response3.GetComponent<CanvasGroup>().alpha = 1;
        response3.GetComponent<CanvasGroup>().blocksRaycasts = true;

        response3.GetComponentInChildren<Text>().text = text;
    }

}

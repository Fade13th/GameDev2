using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour {
    public Sprite onState;
    public Sprite offState;

    public GameObject disablable;

    public bool enabled = true;

    private SpriteRenderer renderer;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();

        setSprite();
    }

    private void setSprite() {
        disablable.SetActive(enabled);
        renderer.sprite = enabled ? onState : offState;
    }

    public void toggle() {
        enabled = !enabled;

        setSprite();
    }
}

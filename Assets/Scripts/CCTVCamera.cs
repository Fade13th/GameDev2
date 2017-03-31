using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCamera : MonoBehaviour {
    public float rotStart = 0;
    public float rotEnd = 120;

    private float currRot;

    public float rotSpeed = 0.5f;

    public FoVController fov;

    private SpriteRenderer renderer;

    public float rotationPause = 1.0f;
    private float time;

    void Start() {
        currRot = rotStart;
        renderer = GetComponent<SpriteRenderer>();

        time = Time.time;
    }

	// Update is called once per frame
	void Update () {
        if (time > Time.time)
            return;

        currRot += rotSpeed;

        fov.Rotation = rotSpeed;

        if (currRot > 90) {
            renderer.flipX = true;
        }
        else {
            renderer.flipX = false;
        }

		if (currRot >= rotEnd || currRot <= 0) {
            rotSpeed = -rotSpeed;

            time = Time.time + rotationPause;
        }  
	}
}

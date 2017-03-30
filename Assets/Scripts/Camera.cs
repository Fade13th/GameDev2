using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public float rotStart = 0;
    public float rotEnd = 120;

    private float currRot;

    public float rotSpeed = 0.5f;

    public FoVRender fov;

    private SpriteRenderer renderer;

    void Start() {
        currRot = rotStart;
        renderer = GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
        currRot += rotSpeed;


        if (currRot > 90) {
            renderer.flipX = true;
        }
        else {
            renderer.flipX = false;
        }

		if (currRot >= rotEnd || currRot <= 0) {
            rotSpeed = -rotSpeed;
        }  
	}
}

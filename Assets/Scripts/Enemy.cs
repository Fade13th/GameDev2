﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {

    public float speed;
    private float moveVelocity;

    public Animator anim;

    private System.Random rand;

    private int lastDirection = -1;

    public float walkTimeUpper;
    public float walkTimeLower;
    public int walkChance = 100;
    private bool walking = false;
    private float time;

    private float FOVXOffset, FOVYOffset, FOVZOffset;

    private float currentVelocity = 0;

    // Use this for initialization
    void Start () {
        rand = new System.Random();
        FOVXOffset = 0.7f;
        FOVYOffset = 0f;
        FOVZOffset = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        moveVelocity = 0;

        if (walking) {
            if (time > Time.time) {
                moveVelocity = currentVelocity; 
            }
            else {
                currentVelocity = 0;
                walking = false;
                anim.SetInteger("Direction", 0);
            }
        }

        //Moves back and forth a set amount at random intervals
        if (!walking && rand.Next(0, walkChance) == 1) {
            lastDirection = -lastDirection;
            moveVelocity += lastDirection * speed;
            FovController fov = GetComponentInChildren<FovController>();
            fov.transform.rotation = Quaternion.Euler(0, 0, 90 * lastDirection);
            fov.transform.localPosition = new Vector3(lastDirection * FOVXOffset, FOVYOffset, FOVZOffset);

            if (lastDirection > 0) {
                anim.SetInteger("Direction", 3);
            }
            else {
                anim.SetInteger("Direction", 1);
            }

            currentVelocity = moveVelocity;
            walking = true;
            time = Time.time + (float)( rand.NextDouble() * (walkTimeUpper - walkTimeLower) + walkTimeLower);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
    }
}

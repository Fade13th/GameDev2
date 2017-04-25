using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage: MonoBehaviour
{
    bool hasCollided = false;
    public String Message;
    public Level level;

    void Start()
    {
        level = GetComponentInParent<Level>();
    }


    void OnGUI()
    {
        if (hasCollided && level.getStage() == 0)
        {
            GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 120), Message);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.Equals("Player"))
        {
            hasCollided = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag.Equals("Player"))
        {
            hasCollided = false;
        }
    }
}